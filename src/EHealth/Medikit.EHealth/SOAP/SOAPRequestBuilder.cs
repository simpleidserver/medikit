// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Extensions;
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.SOAP.DTOs;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;

namespace Medikit.EHealth.SOAP
{
    public class SOAPRequestBuilder<T> where T : SOAPBody
    {
        private readonly SOAPEnvelope<T> _envelope;

        private SOAPRequestBuilder(T body)
        {
            _envelope = new SOAPEnvelope<T>
            {
                Body = body
            };
        }

        public static SOAPRequestBuilder<T> New(T body)
        {
            return new SOAPRequestBuilder<T>(body);
        }

        public SOAPRequestBuilder<T> AddTimestamp(DateTime issueInstant)
        {
            return AddTimestamp(issueInstant, issueInstant.AddHours(1));
        }

        public SOAPRequestBuilder<T> AddTimestamp(DateTime issueInstant, DateTime expirationTime)
        {
            EnsureSecurityHeaderExists();
            var tsId = $"ts-{Guid.NewGuid().ToString()}";
            _envelope.Header.Security.Timestamp = new SOAPTimestamp
            {
                Created = issueInstant,
                Expires = issueInstant.AddHours(1),
                Id = tsId
            };
            return this;
        }

        public SOAPRequestBuilder<T> AddBinarySecurityToken(X509Certificate2 certificate)
        {
            EnsureSecurityHeaderExists();
            var x509Id = $"x509-{Guid.NewGuid().ToString()}";
            var cert = Convert.ToBase64String(certificate.Export(X509ContentType.Cert));
            _envelope.Header.Security.BinarySecurityToken = new SOAPBinarySecurityToken
            {
                EncodingType = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary",
                ValueType = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3",
                Id = x509Id,
                Content = cert
            };
            return this;
        }

        public SOAPRequestBuilder<T> AddSAMLAssertion(SAMLAssertion assertion)
        {
            EnsureSecurityHeaderExists();
            _envelope.Header.Security.Assertion = assertion;
            return this;
        }

        public SOAPRequestBuilder<T> AddReferenceToBinarySecurityToken()
        {
            EnsureSignatureHeaderExists();
            var x509Id = _envelope.Header.Security.BinarySecurityToken.Id;
            var kiId = $"ki-{Guid.NewGuid().ToString()}";
            var strId = $"str-{Guid.NewGuid().ToString()}";
            _envelope.Header.Security.Signature.KeyInfo = new SOAPKeyInfo
            {
                Id = kiId,
                SecurityTokenReference = new SOAPSecurityTokenReference
                {
                    Id = strId,
                    Reference = new SOAPKeyInfoReference
                    {
                        Uri = $"#{x509Id}",
                        ValueType = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"
                    }
                }
            };
            return this;
        }

        public SOAPRequestBuilder<T> AddReferenceToSAMLAssertion()
        {
            EnsureSignatureHeaderExists();
            var kiId = $"ki-{Guid.NewGuid().ToString()}";
            var strId = $"str-{Guid.NewGuid().ToString()}";
            _envelope.Header.Security.Signature.KeyInfo = new SOAPKeyInfo
            {
                Id = kiId,
                SecurityTokenReference = new SOAPSecurityTokenReference
                {
                    Id = strId,
                    TokenType = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1",
                    KeyIdentifier = new SOAPKeyIdentifier
                    {
                        ValueType = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.0#SAMLAssertionID",
                        Content = _envelope.Header.Security.Assertion.AssertionId
                    }
                }
            };
            return this;
        }

        public SOAPRequestBuilder<T> SignWithCertificate(X509Certificate2 certificate)
        {
            EnsureSignatureHeaderExists();
            var ids = new List<string>
            {
                _envelope.Body.Id
            };
            if (_envelope.Header != null && _envelope.Header.Security != null)
            {
                if (_envelope.Header.Security.Timestamp != null)
                {
                    ids.Add(_envelope.Header.Security.Timestamp.Id);
                }

                if (_envelope.Header.Security.BinarySecurityToken != null)
                {
                    ids.Add(_envelope.Header.Security.BinarySecurityToken.Id);
                }
            }

            var xml = _envelope.Serialize().SerializeToString();
            var signedInfo = CanonicalizeHelper.Canonicalize(xml, ids, new List<Transform>
            {
                new XmlDsigExcC14NTransform()
            });
            var payload = signedInfo.ComputeSignature();
            var privateKey = certificate.GetRSAPrivateKey();
            var signaturePayload = privateKey.SignData(payload, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
            var sigId = $"sig-{Guid.NewGuid().ToString()}";
            _envelope.Header.Security.Signature.SignatureValue = Convert.ToBase64String(signaturePayload);
            _envelope.Header.Security.Signature.Id = sigId;
            _envelope.Header.Security.Signature.SignedInfo = signedInfo;
            return this;
        }

        public SOAPEnvelope<T> Build()
        {
            return _envelope;
        }
        

        private void EnsureSecurityHeaderExists()
        {
            if (_envelope.Header == null)
            {
                _envelope.Header = new SOAPHeader();
            }

            if (_envelope.Header.Security == null)
            {
                _envelope.Header.Security = new SOAPSecurity
                {
                    MustUnderstand = 1
                };
            }
        }

        private void EnsureSignatureHeaderExists()
        {
            EnsureSecurityHeaderExists();
            if (_envelope.Header.Security.Signature == null)
            {
                _envelope.Header.Security.Signature = new SOAPSignature();
            }
        }
    }
}
