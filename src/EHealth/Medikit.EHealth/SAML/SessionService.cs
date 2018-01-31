﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Extensions;
using Medikit.EHealth.KeyStore;
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.SOAP;
using Medikit.EHealth.SOAP.DTOs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace Medikit.EHealth.SAML
{
    public class SessionService : ISessionService
    {
        private static SOAPEnvelope<SAMLResponseBody> _cachedSession;
        private static Dictionary<string, string> MAPPING_NAME_TO_OID = new Dictionary<string, string>
        {
            { "SERIALNUMBER=", "OID.2.5.4.5=" },
            { "G=", "OID.2.5.4.42=" },
            { "SN=", "OID.2.5.4.4=" }
        };
        private readonly IKeyStoreManager _keyStoreManager;
        private readonly ISOAPClient _soapClient;
        private readonly EHealthOptions _options;

        public SessionService(IKeyStoreManager keyStoreManager, ISOAPClient soapClient, IOptions<EHealthOptions> options)
        {
            _keyStoreManager = keyStoreManager;
            _soapClient = soapClient;
            _options = options.Value;
        }

        public SOAPEnvelope<SAMLResponseBody> GetSession()
        {
            return _cachedSession;
        }

        public async Task<SOAPEnvelope<SAMLResponseBody>> BuildFallbackSession()
        {
            var request = BuildFallbackSAMLRequest();
            var httpResult = await _soapClient.Send(request, new Uri(_options.StsUrl), "urn:be:fgov:ehealth:sts:protocol:v1:RequestSecureToken");
            httpResult.EnsureSuccessStatusCode();
            var xml = await httpResult.Content.ReadAsStringAsync();
            _cachedSession = SOAPEnvelope<SAMLResponseBody>.Deserialize(xml);
            return _cachedSession;
        }

        private SOAPEnvelope<SAMLRequestBody> BuildFallbackSAMLRequest()
        {
            var idAuthCertificate = _keyStoreManager.GetIdAuthCertificate();
            var orgAuthCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var samlAttributes = new List<SAMLAttribute>
            {
                new SAMLAttribute
                {
                    AttributeName = "urn:be:fgov:ehealth:1.0:certificateholder:person:ssin",
                    AttributeNamespace = "urn:be:fgov:identification-namespace",
                    AttributeValue = idAuthCertificate.ExtractSSIN()
                },
                new SAMLAttribute
                {
                    AttributeName = "urn:be:fgov:person:ssin",
                    AttributeNamespace = "urn:be:fgov:identification-namespace",
                    AttributeValue = idAuthCertificate.ExtractSSIN()
                },
                new SAMLAttribute
                {
                    AttributeName = _options.IdentityProfession.Code,
                    AttributeNamespace = "urn:be:fgov:certified-namespace:ehealth"
                }
            };
            var issueInstant = DateTime.UtcNow;
            var majorVersion = 1;
            var minorVersion = 1;
            var bodyId = $"id-{Guid.NewGuid().ToString()}";
            var requestId = $"request-{Guid.NewGuid().ToString()}";
            var assertionId = $"assertion-{Guid.NewGuid().ToString()}";
            var userSubject = FormatSubject(idAuthCertificate.Subject);
            var issuerSubject = FormatSubject(idAuthCertificate.Issuer);
            var nameIdentifier = new SAMLNameIdentifier
            {
                Format = "urn:oasis:names:tc:SAML:1.1:nameid-format:X509SubjectName",
                NameQualifier = issuerSubject,
                Content = userSubject
            };
            var samlRequest = new SAMLRequest
            {
                MajorVersion = majorVersion,
                MinorVersion = minorVersion,
                RequestId = requestId,
                IssueInstant = issueInstant,
                AttributeQuery = new SAMLAttributeQuery
                {
                    Subject = new SAMLSubject
                    {
                        NameIdentifier = nameIdentifier,
                        SubjectConfirmation = new SAMLSubjectConfirmation
                        {
                            ConfirmationMethod = "urn:oasis:names:tc:SAML:1.0:cm:holder-of-key",
                            SubjectConfirmationData = new SAMLSubjectConfirmationData
                            {
                                Assertion = new SAMLAssertion
                                {
                                    IssueInstant = issueInstant,
                                    AssertionId = assertionId,
                                    Issuer = userSubject,
                                    MajorVersion = majorVersion,
                                    MinorVersion = minorVersion,
                                    Conditions = new SAMLConditions
                                    {
                                        NotBefore = issueInstant,
                                        NotOnOrAfter = issueInstant.AddDays(1)
                                    },
                                    AttributeStatement = new SAMLAttributeStatement
                                    {
                                        Subject = new SAMLSubject
                                        {
                                            NameIdentifier = nameIdentifier
                                        },
                                        Attribute = samlAttributes.Where(_ => !string.IsNullOrWhiteSpace(_.AttributeValue)).ToList()
                                    }
                                }
                            },
                            KeyInfo = new SOAPKeyInfo
                            {
                                X509Data = new SOAPX509Data
                                {
                                    X509Certificate = Convert.ToBase64String(orgAuthCertificate.Export(X509ContentType.Cert))
                                }
                            }
                        }
                    },
                    AttributeDesignator = samlAttributes.Select(a => new SAMLAttributeDesignator
                    {
                        AttributeName = a.AttributeName,
                        AttributeNamespace = a.AttributeNamespace
                    }).ToList()
                }
            };
            var samlRequestBody = new SAMLRequestBody
            {
                Id = bodyId,
                Request = samlRequest
            };
            var signedInfo = CanonicalizeHelper.Canonicalize(samlRequestBody.Serialize().SerializeToString(), new List<string>
            {
                samlRequest.RequestId
            }, new List<Transform>
            {
                new XmlDsigEnvelopedSignatureTransform(),
                new XmlDsigExcC14NTransform()
            });
            var payload = signedInfo.ComputeSignature();
            var privateKey = orgAuthCertificate.GetRSAPrivateKey();
            var signaturePayload = privateKey.SignData(payload, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
            var signatureStr = Convert.ToBase64String(signaturePayload);
            samlRequest.Signature = new SOAPSignature
            {
                KeyInfo = new SOAPKeyInfo
                {
                    X509Data = new SOAPX509Data
                    {
                        X509Certificate = Convert.ToBase64String(orgAuthCertificate.Export(X509ContentType.Cert))
                    }
                },
                SignatureValue = signatureStr,
                SignedInfo = signedInfo
            };
            return SOAPRequestBuilder<SAMLRequestBody>.New(samlRequestBody)
                .AddTimestamp(issueInstant)
                .AddBinarySecurityToken(idAuthCertificate)
                .AddReferenceToBinarySecurityToken()
                .SignWithCertificate(idAuthCertificate)
                .Build();
        }

        private static string FormatSubject(string subject)
        {
            foreach (var kvp in MAPPING_NAME_TO_OID)
            {
                subject = subject.Replace(kvp.Key, kvp.Value);
            }

            return subject;
        }
    }
}