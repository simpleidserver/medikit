﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.KeyStore;
using Medikit.EHealth.Services.DICS.Request;
using Medikit.EHealth.Services.DICS.Response;
using Medikit.EHealth.SOAP;
using Medikit.EHealth.SOAP.DTOs;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Medikit.EHealth.Services.DICS
{
    public class DicsService : IDicsService
    {
        private readonly IKeyStoreManager _keyStoreManager;
        private readonly EHealthOptions _options;
        private readonly ISOAPClient _soapClient;

        public DicsService(IKeyStoreManager keyStoreManager, ISOAPClient client, IOptions<EHealthOptions> options)
        {
            _keyStoreManager = keyStoreManager;
            _soapClient = client;
            _options = options.Value;
        }

        public async Task FindAmpp(DICSFindAmppRequest request)
        {
            var issueInstant = DateTime.UtcNow;
            request.IssueInstant = issueInstant;
            var orgAuthCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var soapRequest = SOAPRequestBuilder<DICSFindAmppRequestBody>.New(new DICSFindAmppRequestBody
            {
                Id = $"id-{Guid.NewGuid().ToString()}",
                Request = request
            })
                .AddTimestamp(issueInstant, issueInstant.AddHours(1))
                .AddBinarySecurityToken(orgAuthCertificate.Certificate)
                .AddReferenceToBinarySecurityToken()
                .SignWithCertificate(orgAuthCertificate)
                .Build();
            var httpResult = await _soapClient.Send(soapRequest, new Uri(_options.DicsUrl), "urn:be:fgov:ehealth:dics:protocol:v5:findAmpp");
            var xml = await httpResult.Content.ReadAsStringAsync();
            httpResult.EnsureSuccessStatusCode();
        }

        public async Task<SOAPEnvelope<DICSFindAmpResponseBody>> FindAmp(DICSFindAmpRequest request)
        {
            var issueInstant = DateTime.UtcNow;
            request.IssueInstant = issueInstant;
            var orgAuthCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var soapRequest = SOAPRequestBuilder<DICSFindAmpRequestBody>.New(new DICSFindAmpRequestBody
            {
                Id = $"id-{Guid.NewGuid().ToString()}",
                Request = request
            })
                .AddTimestamp(issueInstant, issueInstant.AddHours(1))
                .AddBinarySecurityToken(orgAuthCertificate.Certificate)
                .AddReferenceToBinarySecurityToken()
                .SignWithCertificate(orgAuthCertificate)
                .Build();
            var httpResult = await _soapClient.Send(soapRequest, new Uri(_options.DicsUrl), "urn:be:fgov:ehealth:dics:protocol:v5:findAmp");
            var xml = await httpResult.Content.ReadAsStringAsync();
            httpResult.EnsureSuccessStatusCode();
            return SOAPEnvelope<DICSFindAmpResponseBody>.Deserialize(xml);
        }

        public async Task FindReimbursement(DICSFindReimbursementRequest request)
        {
            var issueInstant = DateTime.UtcNow;
            request.IssueInstant = issueInstant;
            var orgAuthCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var soapRequest = SOAPRequestBuilder<DICSFindReimbursementRequestBody>.New(new DICSFindReimbursementRequestBody
            {
                Id = $"id-{Guid.NewGuid().ToString()}",
                Request = request
            })
                .AddTimestamp(issueInstant, issueInstant.AddHours(1))
                .AddBinarySecurityToken(orgAuthCertificate.Certificate)
                .AddReferenceToBinarySecurityToken()
                .SignWithCertificate(orgAuthCertificate)
                .Build();
            var httpResult = await _soapClient.Send(soapRequest, new Uri(_options.DicsUrl), "urn:be:fgov:ehealth:dics:protocol:v5:findReimbursement");
            var xml = await httpResult.Content.ReadAsStringAsync();
            httpResult.EnsureSuccessStatusCode();
        }
    }
}