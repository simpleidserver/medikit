// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.ETK;
using Medikit.EHealth.Extensions;
using Medikit.EHealth.KeyStore;
using Medikit.EHealth.Pkcs;
using Medikit.EHealth.SAML;
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.Services.KGSS;
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using Medikit.EHealth.Services.Recipe.Request;
using Medikit.EHealth.Services.Recipe.Response;
using Medikit.EHealth.SOAP;
using Medikit.EHealth.SOAP.DTOs;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Medikit.EHealth.Services.Recipe
{
    public class RecipeService : IRecipeService
    {
        private readonly IETKService _etkService;
        private readonly IKGSSService _kgssService;
        private readonly IKeyStoreManager _keyStoreManager;
        private readonly ISOAPClient _soapClient;
        private readonly ISessionService _sessionService;
        private readonly EHealthOptions _options;

        public RecipeService(IETKService etkService, IKGSSService kgssService, IKeyStoreManager keyStoreManager, ISOAPClient soapClient, ISessionService sessionService, IOptions<EHealthOptions> options)
        {
            _etkService = etkService;
            _kgssService = kgssService;
            _keyStoreManager = keyStoreManager;
            _soapClient = soapClient;
            _sessionService = sessionService;
            _options = options.Value;
        }

        public Task<GetPrescriptionResult> GetPrescription(string rid)
        {
            var assertion = _sessionService.GetSession().Body.Response.Assertion;
            return GetPrescription(rid, assertion);
        }

        public async Task<GetPrescriptionResult> GetPrescription(string rid, SAMLAssertion assertion)
        {
            var orgCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var issueInstant = DateTime.UtcNow;
            var recipeETK = await _etkService.GetRecipeETK();
            var symKey = new TripleDESCryptoServiceProvider
            {
                Padding = PaddingMode.None,
                Mode = CipherMode.ECB
            };
            var getPrescriptionParameter = new GetPrescriptionForPrescriberParameter
            {
                Rid = rid,
                SymmKey = Convert.ToBase64String(symKey.Key)
            };
            var serializedPrescriptionParameter = Encoding.UTF8.GetBytes(getPrescriptionParameter.Serialize().SerializeToString(false, true));
            byte[] sealedContent = TripleWrapper.Seal(serializedPrescriptionParameter, orgCertificate, recipeETK.Certificate);
            var getPrescriptionRequest = new GetPrescriptionRequest
            {
                Id = $"id{Guid.NewGuid().ToString()}",
                IssueInstant = issueInstant,
                ProgramId = _options.ProductName,
                SecuredGetPrescriptionRequest = new SecuredContentType
                {
                    SecuredContent = Convert.ToBase64String(sealedContent)
                }
            };

            var soapRequest = SOAPRequestBuilder<GetPrescriptionRequestBody>.New(new GetPrescriptionRequestBody
            {
                Id = $"id-{Guid.NewGuid().ToString()}",
                Request = getPrescriptionRequest
            })
                .AddTimestamp(issueInstant, issueInstant.AddHours(1))
                .AddSAMLAssertion(assertion)
                .AddReferenceToSAMLAssertion()
                .SignWithCertificate(orgCertificate)
                .Build();
            var result = await _soapClient.Send(soapRequest, new Uri(_options.PrescriberUrl), "urn:be:fgov:ehealth:recipe:protocol:v4:getPrescription");
            var xml = await result.Content.ReadAsStringAsync();
            result.EnsureSuccessStatusCode();
            var response = SOAPEnvelope<GetPrescriptionResponseBody>.Deserialize(xml);
            var securedContent = response.Body.GetPrescriptionResponse.SecuredGetPrescriptionResponse.SecuredContent;
            byte[] decrypted;
            using (var decryptor = symKey.CreateDecryptor())
            {
                var payload = Convert.FromBase64String(securedContent);
                decrypted = decryptor.TransformFinalBlock(payload, 0, payload.Length);
            }

            xml = Encoding.UTF8.GetString(decrypted).ClearBadFormat();
            var prescriptionResult = GetPrescriptionForPrescriberResult.Deserialize(xml);
            var kgssResponse = await _kgssService.GetKeyFromKGSS(prescriptionResult.EncryptionKeyId, assertion);
            var unsealed = TripleWrapper.Unseal(Convert.FromBase64String(prescriptionResult.Prescription), Convert.FromBase64String(kgssResponse.NewKey));
            var decompressed = Decompress(unsealed);
            return new GetPrescriptionResult
            {
                Status = prescriptionResult.Status.Code,
                CreationDate = prescriptionResult.CreationDate,
                FeedbackAllowed = prescriptionResult.FeedbackAllowed,
                PatientId = prescriptionResult.PatientId,
                ExpirationDate = prescriptionResult.ExpirationDate,
                Rid = prescriptionResult.Rid,
                KmehrmessageType = Encoding.UTF8.GetString(decompressed).Deserialize<kmehrmessageType>()
            };
        }        

        public async Task<CreatePrescriptionResult> CreatePrescription(string prescriptionType, string patientId, DateTime expirationDateTime, kmehrmessageType msgType, SAMLAssertion assertion)
        {
            var orgCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var recipeETK = await _etkService.GetRecipeETK();
            var kgssResponse = await _kgssService.GetKGSS(new System.Collections.Generic.List<CredentialType>
            {
                new CredentialType
                {
                    Namespace = Constants.AttributeStatementNamespaces.Identification,
                    Name = Constants.AttributeStatementNames.PersonSSIN,
                    Value = assertion.AttributeStatement.Attribute.First(_ => _.AttributeNamespace == EHealth.Constants.AttributeStatementNamespaces.Identification).AttributeValue
                },
                new CredentialType
                {
                    Namespace = Constants.AttributeStatementNamespaces.Certified,
                    Name = assertion.AttributeStatement.Attribute.First(_ => _.AttributeNamespace == EHealth.Constants.AttributeStatementNamespaces.Certified).AttributeName
                }
            });
            var prescriptionPayload = msgType.SerializeToByte(false, true);
            var compressedPayload = Compress(prescriptionPayload);
            byte[] encryptedPayload;
            using (var aes = Aes.Create())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                aes.KeySize = 128;
                aes.Key = Convert.FromBase64String(kgssResponse.NewKey);
                encryptedPayload = TripleWrapper.Seal(compressedPayload, orgCertificate, kgssResponse.NewKeyIdentifier, aes);
            }

            var symKey = new TripleDESCryptoServiceProvider
            {
                Padding = PaddingMode.None,
                Mode = CipherMode.ECB
            };
            var prescriptionParameter = new CreatePrescriptionParameter
            {
                Prescription = Convert.ToBase64String(encryptedPayload),
                PrescriptionType = prescriptionType,
                FeedbackRequested = false,
                KeyId = kgssResponse.NewKeyIdentifier,
                SymmKey = Convert.ToBase64String(symKey.Key),
                PatientId = patientId,
                ExpirationDate = expirationDateTime.ToString("yyyy-MM-dd"),
                Vision = ""
            };
            var serializedPrescriptionParameter = Encoding.UTF8.GetBytes(prescriptionParameter.Serialize().SerializeToString(false, true));
            byte[] sealedContent = TripleWrapper.Seal(serializedPrescriptionParameter, orgCertificate, recipeETK.Certificate);
            var issueInstant = DateTime.UtcNow;
            var createPrescriptionRequest = new CreatePrescriptionRequest
            {
                IssueInstant = issueInstant,
                Id = $"id{Guid.NewGuid().ToString()}",
                ProgramId = "Medikit",
                AdministrativeInformation = new CreatePrescriptionAdministrativeInformationType
                {
                    KeyIdentifier = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(kgssResponse.NewKeyIdentifier)),
                    PrescriptionVersion = "kmehr_1.29",
                    ReferenceSourceVersion = "samv2:ABCDE999999999999",
                    PrescriptionType = prescriptionType
                },
                SecuredCreatePrescriptionRequest = new SecuredContentType
                {
                    SecuredContent = Convert.ToBase64String(sealedContent)
                }
            };
            var soapRequest = SOAPRequestBuilder<CreatePrescriptionRequestBody>.New(new CreatePrescriptionRequestBody
            {
                Id = $"id-{Guid.NewGuid().ToString()}",
                Request = createPrescriptionRequest
            })
                .AddTimestamp(issueInstant, issueInstant.AddHours(1))
                .AddSAMLAssertion(assertion)
                .AddReferenceToSAMLAssertion()
                .SignWithCertificate(orgCertificate)
                .Build();
            var result = await _soapClient.Send(soapRequest, new Uri(_options.PrescriberUrl), "urn:be:fgov:ehealth:recipe:protocol:v4:createPrescription");
            var xml = await result.Content.ReadAsStringAsync();
            result.EnsureSuccessStatusCode();
            var response = SOAPEnvelope<CreatePrescriptionResponseBody>.Deserialize(xml);
            var securedContent = response.Body.CreatePrescriptionResponse.SecuredGetPrescriptionResponse.SecuredContent;
            byte[] decrypted;
            using (var decryptor = symKey.CreateDecryptor())
            {
                var payload = Convert.FromBase64String(securedContent);
                decrypted = decryptor.TransformFinalBlock(payload, 0, payload.Length);
            }

            xml = Encoding.UTF8.GetString(decrypted);
            xml = xml.ClearBadFormat();
            return CreatePrescriptionResult.Deserialize(xml);
        }

        public Task<ListOpenRidsResult> GetOpenedPrescriptions(string patientId, Page page)
        {
            var assertion = _sessionService.GetSession().Body.Response.Assertion;
            return GetOpenedPrescriptions(patientId, page, assertion);
        }

        public async Task<ListOpenRidsResult> GetOpenedPrescriptions(string patientId, Page page, SAMLAssertion assertion)
        {
            var orgCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var issueInstant = DateTime.UtcNow;
            var recipeETK = await _etkService.GetRecipeETK();
            var symKey = new TripleDESCryptoServiceProvider();
            symKey.Padding = PaddingMode.None;
            symKey.Mode = CipherMode.ECB;
            var lsitOpenPrescriptionParameter = new ListOpenPrescriptionParameter
            {
                PatientId = patientId,
                Page = page,
                SymmKey = Convert.ToBase64String(symKey.Key)
            };
            var serializedPrescriptionParameter = Encoding.UTF8.GetBytes(lsitOpenPrescriptionParameter.Serialize().SerializeToString(false, true));
            byte[] sealedContent = TripleWrapper.Seal(serializedPrescriptionParameter, orgCertificate, recipeETK.Certificate);
            var listOpenPrescriptionRequest = new ListOpenPrescriptionsRequest
            {
                Id = $"id{Guid.NewGuid().ToString()}",
                IssueInstant = issueInstant,
                ProgramId = _options.ProductName,
                SecuredListOpenRidsRequest = new SecuredContentType
                {
                    SecuredContent = Convert.ToBase64String(sealedContent)
                }
            };

            var soapRequest = SOAPRequestBuilder<ListOpenPrescriptionRequestBody>.New(new ListOpenPrescriptionRequestBody
            {
                Id = $"id-{Guid.NewGuid().ToString()}",
                Request = listOpenPrescriptionRequest
            })
                .AddTimestamp(issueInstant, issueInstant.AddHours(1))
                .AddSAMLAssertion(assertion)
                .AddReferenceToSAMLAssertion()
                .SignWithCertificate(orgCertificate)
                .Build();
            var result = await _soapClient.Send(soapRequest, new Uri(_options.PrescriberUrl), "urn:be:fgov:ehealth:recipe:protocol:v4:ListOpenRids");
            result.EnsureSuccessStatusCode();
            var xml = await result.Content.ReadAsStringAsync();
            var response = SOAPEnvelope<ListOpenPrescriptionResponseBody>.Deserialize(xml);
            var securedContent = response.Body.ListOpenPrescriptionResponse.SecuredListOpenRidsResponse.SecuredContent;
            byte[] decrypted;
            using (var decryptor = symKey.CreateDecryptor())
            {
                var payload = Convert.FromBase64String(securedContent);
                decrypted = decryptor.TransformFinalBlock(payload, 0, payload.Length);
            }

            xml = Encoding.UTF8.GetString(decrypted);
            xml = xml.ClearBadFormat();
            var res = ListOpenRidsResult.Deserialize(xml);
            return res;
        }

        public Task<RevokePrescriptionResult> RevokePrescription(string rid, string reason)
        {
            var assertion = _sessionService.GetSession().Body.Response.Assertion;
            return RevokePrescription(rid, reason, assertion);
        }

        public async Task<RevokePrescriptionResult> RevokePrescription(string rid, string reason, SAMLAssertion assertion)
        {
            var orgCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var issueInstant = DateTime.UtcNow;
            var recipeETK = await _etkService.GetRecipeETK();
            var symKey = new TripleDESCryptoServiceProvider();
            symKey.Padding = PaddingMode.None;
            symKey.Mode = CipherMode.ECB;
            var revokePrescriptionParameter = new RevokePrescriptionParameter
            {
                Reason = reason,
                Rid = rid,
                SymmKey = Convert.ToBase64String(symKey.Key)
            };
            var serializedPrescriptionParameter = Encoding.UTF8.GetBytes(revokePrescriptionParameter.Serialize().SerializeToString(false, true));
            byte[] sealedContent = TripleWrapper.Seal(serializedPrescriptionParameter, orgCertificate, recipeETK.Certificate);
            var revokePrescriptionRequest = new RevokePrescriptionRequest
            {
                Id = $"id{Guid.NewGuid().ToString()}",
                IssueInstant = issueInstant,
                ProgramId = _options.ProductName,
                SecuredRevokePrescriptionRequest = new SecuredContentType
                {
                    SecuredContent = Convert.ToBase64String(sealedContent)
                }
            };

            var soapRequest = SOAPRequestBuilder<RevokePrescriptionRequestBody>.New(new RevokePrescriptionRequestBody
            {
                Id = $"id-{Guid.NewGuid().ToString()}",
                Request = revokePrescriptionRequest
            })
                .AddTimestamp(issueInstant, issueInstant.AddHours(1))
                .AddSAMLAssertion(assertion)
                .AddReferenceToSAMLAssertion()
                .SignWithCertificate(orgCertificate)
                .Build();
            var result = await _soapClient.Send(soapRequest, new Uri(_options.PrescriberUrl), "urn:be:fgov:ehealth:recipe:protocol:v4:revokePrescription");
            var xml = await result.Content.ReadAsStringAsync();
            result.EnsureSuccessStatusCode();
            var response = SOAPEnvelope<RevokePrescriptionResponseBody>.Deserialize(xml);
            var securedContent = response.Body.RevokePrescriptionResponse.SecuredRevokePrescriptionResponse.SecuredContent;
            byte[] decrypted;
            using (var decryptor = symKey.CreateDecryptor())
            {
                var payload = Convert.FromBase64String(securedContent);
                decrypted = decryptor.TransformFinalBlock(payload, 0, payload.Length);
            }

            xml = Encoding.UTF8.GetString(decrypted);
            xml = xml.ClearBadFormat();
            var res = RevokePrescriptionResult.Deserialize(xml);
            return res;
        }

        private static byte[] Compress(byte[] input)
        {
            using (var compressedStream = new MemoryStream())
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                zipStream.Write(input, 0, input.Length);
                zipStream.Close();
                return compressedStream.ToArray();
            }
        }

        private static byte[] Decompress(byte[] input)
        {
            using (var to = new MemoryStream())
            {
                using (var from = new MemoryStream(input))
                {
                    using (var stream = new GZipStream(from, CompressionMode.Decompress))
                    {
                        stream.CopyTo(to);
                        return to.ToArray();
                    }
                }
            }
        }
    }
}
