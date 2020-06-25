// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.ETK.Request;
using Medikit.EHealth.ETK.Response;
using Medikit.EHealth.ETK.Store;
using Medikit.EHealth.Extensions;
using Medikit.EHealth.KeyStore;
using Medikit.EHealth.Services.ETK.Store;
using Medikit.EHealth.SOAP;
using Medikit.EHealth.SOAP.DTOs;
using Medikit.Security.Cryptography.Pkcs;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Medikit.EHealth.ETK
{
    public class ETKService : IETKService
    {
        private readonly IETKStore _etkStore;
        private readonly ISOAPClient _soapClient;
        private readonly IKeyStoreManager _keyStoreManager;
        private readonly EHealthOptions _options;

        public ETKService(IETKStore etkStore, ISOAPClient soapClient, IKeyStoreManager keyStoreManager, IOptions<EHealthOptions> options)
        {
            _etkStore = etkStore;
            _soapClient = soapClient;
            _keyStoreManager = keyStoreManager;
            _options = options.Value;
        }

        public Task<ETKModel> GetOrgETK()
        {
            var cbe = _keyStoreManager.GetOrgAuthCertificate().Certificate.ExtractCBE();
            return GetETK(_options.OrgType, cbe);
        }

        public Task<ETKModel> GetRecipeETK()
        {
            return GetETK(ETKIdentifier.RECIPE_ETK);
        }

        public Task<ETKModel> GetKgssETK()
        {
            return GetETK(ETKIdentifier.KGSS_ETK);
        }

        public Task<ETKModel> GetETK(ETKTypes type, string value, string applicationId = "")
        {
            return GetETK(new ETKIdentifier(Constants.EtkTypeToString[type], value, applicationId));
        }

        public async Task<ETKModel> GetETK(ETKIdentifier etkIdentifier)
        {
            var result = await _etkStore.Get(etkIdentifier.Type, etkIdentifier.Value, etkIdentifier.ApplicationId);
            if (result != null)
            {
                return result;
            }

            var request = new SOAPEnvelope<ETKGetRequestBody>
            {
                Body = new ETKGetRequestBody
                {
                    Request = new ETKGetRequest
                    {
                        SearchCriteria = new ETKSearchCriteria
                        {
                            Identifier = etkIdentifier
                        }
                    }
                }
            };
            var httpResponse = await _soapClient.Send(request, new Uri(_options.EtkUrl), null);
            httpResponse.EnsureSuccessStatusCode();
            var xml = await httpResponse.Content.ReadAsStringAsync();
            var etkResponse = SOAPEnvelope<ETKGetResponseBody>.Deserialize(xml);
            var signedCms = new SignedCms();
            signedCms.Decode(Convert.FromBase64String(etkResponse.Body.GetETKResponse.ETK));
            var cert = new X509Certificate2(signedCms.ContentInfo.Content);
            await _etkStore.Add(etkIdentifier.Type, etkIdentifier.Value, etkIdentifier.ApplicationId, cert, etkResponse.Body.GetETKResponse.ETK);
            result = new ETKModel(cert, etkResponse.Body.GetETKResponse.ETK);
            return result;
        }
    }
}