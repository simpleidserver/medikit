// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.ETK;
using Medikit.EHealth.Extensions;
using Medikit.EHealth.KeyStore;
using Medikit.EHealth.Pkcs;
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.Services.KGSS.Request;
using Medikit.EHealth.Services.KGSS.Response;
using Medikit.EHealth.Services.KGSS.Response.GetKey;
using Medikit.EHealth.SOAP;
using Medikit.EHealth.SOAP.DTOs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Medikit.EHealth.Services.KGSS
{
    public class KGSSService : IKGSSService
    {
        private readonly ISOAPClient _soapClient;
        private readonly IETKService _etkService;
        private readonly IKeyStoreManager _keyStoreManager;
        private readonly EHealthOptions _options;

        public KGSSService(ISOAPClient soapClient, IETKService etkService, IKeyStoreManager keyStoreManager, IOptions<EHealthOptions> options)
        {
            _soapClient = soapClient;
            _etkService = etkService;
            _keyStoreManager = keyStoreManager;
            _options = options.Value;
        }

        public async Task<KGSSGetKeyResponseContent> GetKeyFromKGSS(string keyId, SAMLAssertion assertion)
        {
            var orgAuthCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var orgEtk = await _etkService.GetOrgETK();
            var kgssEtk = await _etkService.GetKgssETK();
            var getKeyRequestContent = new KGSSGetKeyRequestContent
            {
                KeyIdentifier = keyId,
                ETK = orgEtk.ETK
            };
            var contentInfoPayload = Encoding.UTF8.GetBytes(getKeyRequestContent.Serialize().ToString());
            var sealedContentInfoPayload = TripleWrapper.Seal(contentInfoPayload, orgAuthCertificate, kgssEtk.Certificate);
            var issueInstant = DateTime.UtcNow;
            var soapRequest = SOAPRequestBuilder<KGSSGetKeyRequestBody>.New(new KGSSGetKeyRequestBody
            {
                Id = $"id-{Guid.NewGuid().ToString()}",
                Request = new KGSSGetKeyRequest
                {
                    SealedKeyRequest = new KGSSSealedKeyRequest
                    {
                        SealedContent = Convert.ToBase64String(sealedContentInfoPayload)
                    }
                }
            })
                .AddTimestamp(issueInstant, issueInstant.AddHours(1))
                .AddSAMLAssertion(assertion)
                .AddReferenceToSAMLAssertion()
                .SignWithCertificate(orgAuthCertificate)
                .Build();
            var result = await _soapClient.Send(soapRequest, new Uri(_options.KgssUrl), null);
            result.EnsureSuccessStatusCode();
            var xml = await result.Content.ReadAsStringAsync();
            var response = SOAPEnvelope<KGSSGetKeyResponseBody>.Deserialize(xml);
            var certificates = new List<X509Certificate2>
            {
                orgAuthCertificate,
                _keyStoreManager.GetOrgETKCertificate()
            };
            var unsealedPayload = TripleWrapper.Unseal(Convert.FromBase64String(response.Body.GetKeyResponse.SealedKeyResponse.SealedContent), certificates.ToCertificateCollection());
            return KGSSGetKeyResponseContent.Deserialize(unsealedPayload);
        }
        
        public Task<KGSSGetNewKeyResponseContent> GetOrgKGSS()
        {
            var orgAuthCertificate = _keyStoreManager.GetOrgAuthCertificate();
            return GetKGSS(BuildOrgAllowedReaders(orgAuthCertificate));
        }

        public async Task<KGSSGetNewKeyResponseContent> GetKGSS(List<CredentialType> credentials)
        {
            var orgAuthCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var orgEtk = await _etkService.GetOrgETK();
            var kgssEtk = await _etkService.GetKgssETK();
            var getNewRequestContent = new KGSSGetNewKeyRequestContent
            {
                AllowedReaders = credentials,
                ETK = orgEtk.ETK
            };
            var contentInfoPayload = Encoding.UTF8.GetBytes(getNewRequestContent.Serialize().ToString());
            var sealedContentInfoPayload = TripleWrapper.Seal(contentInfoPayload, orgAuthCertificate, kgssEtk.Certificate);
            var request = new SOAPEnvelope<KGSSGetNewKeyRequestBody>
            {
                Body = new KGSSGetNewKeyRequestBody
                {
                    Request = new KGSSGetNewKeyRequest
                    {
                        SealedNewKeyRequest = new KGSSSealedNewKeyRequest
                        {
                            SealedContent = Convert.ToBase64String(sealedContentInfoPayload)
                        }
                    }
                }
            };
            var result = await _soapClient.Send(request, new Uri(_options.KgssUrl), null);
            result.EnsureSuccessStatusCode();
            var xml = await result.Content.ReadAsStringAsync();
            var response = SOAPEnvelope<KGSSGetNewKeyResponseBody>.Deserialize(xml);
            var certificates = new List<X509Certificate2>
            {
                orgAuthCertificate,
                _keyStoreManager.GetOrgETKCertificate()
            };
            var unsealedPayload = TripleWrapper.Unseal(Convert.FromBase64String(response.Body.GetNewKeyResponse.SealedNewKeyResponse.SealedContent), certificates.ToCertificateCollection());
            return KGSSGetNewKeyResponseContent.Deserialize(unsealedPayload);
        }

        private List<CredentialType> BuildOrgAllowedReaders(X509Certificate2 certificate)
        {
            if (_options.OrgType == ETKTypes.CBE)
            {
                return new List<CredentialType>
                {
                    new CredentialType
                    {
                        Namespace = "urn:be:fgov:identification-namespace",
                        Name = "urn:be:fgov:ehealth:1.0:certificateholder:enterprise:cbe-number",
                        Value = certificate.ExtractCBE()
                    },
                    new CredentialType
                    {
                        Namespace = "urn:be:fgov:identification-namespace",
                        Name = "urn:be:fgov:kbo-bce:organization:cbe-number",
                        Value = certificate.ExtractCBE()
                    }
                };
            }

            return null;
        }
    }
}
