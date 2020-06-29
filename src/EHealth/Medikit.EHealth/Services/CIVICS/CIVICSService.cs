// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.KeyStore;
using Medikit.EHealth.Services.CIVICS.Request;
using Medikit.EHealth.SOAP;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Medikit.EHealth.Services.CIVICS
{
    public class CIVICSService : ICIVICSService
    {
        private readonly IKeyStoreManager _keyStoreManager;
        private readonly EHealthOptions _options;
        private readonly ISOAPClient _soapClient;

        public CIVICSService(IKeyStoreManager keyStoreManager, ISOAPClient client, IOptions<EHealthOptions> options)
        {
            _keyStoreManager = keyStoreManager;
            _soapClient = client;
            _options = options.Value;
        }

        public async Task FindCNK(CIVICSFindCNKRequest request)
        {
            var issueInstant = DateTime.UtcNow;
            request.IssueInstant = issueInstant;
            var orgAuthCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var soapRequest = SOAPRequestBuilder<CIVICSFindCNKRequestBody>.New(new CIVICSFindCNKRequestBody
            {
                Id = $"id-{Guid.NewGuid().ToString()}",
                Request = request
            })
                .AddTimestamp(issueInstant, issueInstant.AddHours(1))
                .AddBinarySecurityToken(orgAuthCertificate.Certificate)
                .AddReferenceToBinarySecurityToken()
                .SignWithCertificate(orgAuthCertificate)
                .Build();
            var httpResult = await _soapClient.Send(soapRequest, new Uri(_options.CivicsUrl), "urn:be:fgov:ehealth:civics:protocol:v2:findCNK");
            var xml = await httpResult.Content.ReadAsStringAsync();
            // 

        }
    }
}
