// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Extensions;
using Medikit.EHealth.KeyStore;
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.SOAP;
using Medikit.EHealth.SOAP.DTOs;
using Medikit.EID;
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
            var xml = await httpResult.Content.ReadAsStringAsync();
            httpResult.EnsureSuccessStatusCode();
            _cachedSession = SOAPEnvelope<SAMLResponseBody>.Deserialize(xml);
            return _cachedSession;
        }

        public async Task<SOAPEnvelope<SAMLResponseBody>> BuildEIDSession(string pin)
        {
            var request = BuildEIDSamlRequest(pin);
            var httpResult = await _soapClient.Send(request, new Uri(_options.StsUrl), "urn:be:fgov:ehealth:sts:protocol:v1:RequestSecureToken");
            var xml = await httpResult.Content.ReadAsStringAsync();
            httpResult.EnsureSuccessStatusCode();
            _cachedSession = SOAPEnvelope<SAMLResponseBody>.Deserialize(xml);
            return _cachedSession;
        }

        private SOAPEnvelope<SAMLRequestBody> BuildEIDSamlRequest(string pin)
        {
            SOAPEnvelope<SAMLRequestBody> samlEnv = null;
            var orgAuthCertificate = _keyStoreManager.GetOrgAuthCertificate();
            using (var discovery = new BeIDCardDiscovery())
            {
                var readers = discovery.GetReaders();
                using (var connection = discovery.Connect(readers.First()))
                {
                    var certificate = connection.GetAuthCertificate();
                    var idAuthCertificate = new X509Certificate2(connection.GetAuthCertificate().Export(X509ContentType.Cert));
                    samlEnv = BuildRequest(idAuthCertificate, orgAuthCertificate, (_ =>
                    {
                        byte[] hashPayload = null;
                        using (var sha = new SHA1CryptoServiceProvider())
                        {
                            hashPayload = sha.ComputeHash(_);
                        }

                        return connection.SignWithAuthenticationCertificate(hashPayload, BeIDDigest.Sha1, pin);
                    }));
                }
            }

            return samlEnv;
        }

        private SOAPEnvelope<SAMLRequestBody> BuildFallbackSAMLRequest()
        {
            var idAuthCertificate = _keyStoreManager.GetIdAuthCertificate();
            var orgAuthCertificate = _keyStoreManager.GetOrgAuthCertificate();
            return BuildRequest(idAuthCertificate, orgAuthCertificate, (_ =>
            {
                var privateKey = (RSA)idAuthCertificate.PrivateKey;
                var signaturePayload = privateKey.SignData(_, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
                return signaturePayload;
            }));
        }

        private SOAPEnvelope<SAMLRequestBody> BuildRequest(X509Certificate2 idAuthCertificate, X509Certificate2 orgAuthCertificate, Func<byte[], byte[]> signCallback)
        {
            var samlAttributes = new List<SAMLAttribute>
            {
                new SAMLAttribute
                {
                    AttributeName = Constants.AttributeStatementNames.CertificateHolderPersonSSIN,
                    AttributeNamespace = Constants.AttributeStatementNamespaces.Identification,
                    AttributeValue = idAuthCertificate.ExtractSSIN()
                },
                new SAMLAttribute
                {
                    AttributeName = Constants.AttributeStatementNames.PersonSSIN,
                    AttributeNamespace = Constants.AttributeStatementNamespaces.Identification,
                    AttributeValue = idAuthCertificate.ExtractSSIN()
                },
                new SAMLAttribute
                {
                    AttributeName = _options.IdentityProfession.Code,
                    AttributeNamespace = Constants.AttributeStatementNamespaces.Certified
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
            var privateKey = (RSA)orgAuthCertificate.PrivateKey;
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
                .SignWithCertificate(signCallback)
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
