// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.KeyStore;
using Medikit.EHealth.SAML;
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.Services.EHealthBox.Request;
using Medikit.EHealth.Services.EHealthBox.Response;
using Medikit.EHealth.SOAP;
using Medikit.EHealth.SOAP.DTOs;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Medikit.EHealth.Services.EHealthBox
{
    public class EHealthBoxService : IEHealthBoxService
    {
        private readonly IKeyStoreManager _keyStoreManager;
        private readonly ISOAPClient _soapClient;
        private readonly ISessionService _sessionService;
        private readonly EHealthOptions _options;

        public EHealthBoxService(IKeyStoreManager keyStoreManager, ISOAPClient soapClient, ISessionService sessionService, IOptions<EHealthOptions> options)
        {
            _keyStoreManager = keyStoreManager;
            _soapClient = soapClient;
            _sessionService = sessionService;
            _options = options.Value;
        }

        public Task<SOAPEnvelope<EHealthBoxGetInfoResponseBody>> GetBoxInfo(EHealthBoxGetInfoRequest request)
        {
            var assertion = _sessionService.GetSession().Body.Response.Assertion;
            return GetBoxInfo(request, assertion);
        }

        public async Task<SOAPEnvelope<EHealthBoxGetInfoResponseBody>> GetBoxInfo(EHealthBoxGetInfoRequest request, SAMLAssertion assertion)
        {
            var issueInstant = DateTime.UtcNow;
            var orgCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var soapRequest = SOAPRequestBuilder<EHealthBoxGetInfoRequestBody>.New(new EHealthBoxGetInfoRequestBody
            {
                Id = $"id-{Guid.NewGuid().ToString()}",
                Request = request
            })
                .AddTimestamp(issueInstant, issueInstant.AddHours(1))
                .AddSAMLAssertion(assertion)
                .AddReferenceToSAMLAssertion()
                .SignWithCertificate(orgCertificate)
                .Build();
            var httpResult = await _soapClient.Send(soapRequest, new Uri(_options.EHealthboxConsultation), "urn:be:fgov:ehealth:ehbox:consultation:protocol:v3:getBoxInfo");
            var xml = await httpResult.Content.ReadAsStringAsync();
            httpResult.EnsureSuccessStatusCode();
            var result = SOAPEnvelope<EHealthBoxGetInfoResponseBody>.Deserialize(xml);
            return result;
        }

        public Task<SOAPEnvelope<EHealthBoxGetMessagesListResponseBody>> GetMessagesList(EHealthBoxGetMessagesListRequest request)
        {
            var assertion = _sessionService.GetSession().Body.Response.Assertion;
            return GetMessagesList(request, assertion);
        }

        public async Task<SOAPEnvelope<EHealthBoxGetMessagesListResponseBody>> GetMessagesList(EHealthBoxGetMessagesListRequest request, SAMLAssertion assertion)
        {
            var issueInstant = DateTime.UtcNow;
            var orgCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var soapRequest = SOAPRequestBuilder<EHealthBoxGetMessagesListRequestBody>.New(new EHealthBoxGetMessagesListRequestBody
            {
                Id = $"id-{Guid.NewGuid().ToString()}",
                Request = request
            })
                .AddTimestamp(issueInstant, issueInstant.AddHours(1))
                .AddSAMLAssertion(assertion)
                .AddReferenceToSAMLAssertion()
                .SignWithCertificate(orgCertificate)
                .Build();
            var httpResult = await _soapClient.Send(soapRequest, new Uri(_options.EHealthboxConsultation), "urn:be:fgov:ehealth:ehbox:consultation:protocol:v3:getMessagesList");
            var xml = await httpResult.Content.ReadAsStringAsync();
            httpResult.EnsureSuccessStatusCode();
            var result = SOAPEnvelope<EHealthBoxGetMessagesListResponseBody>.Deserialize(xml);
            return result;
        }

        public Task<SOAPEnvelope<EHealthBoxSendMessageResponseBody>> SendMessage(EHealthBoxSendMessageRequest request)
        {
            var assertion = _sessionService.GetSession().Body.Response.Assertion;
            return SendMessage(request, assertion);
        }

        public Task<SOAPEnvelope<EHealthBoxSendMessageResponseBody>> SendMessage(EHealthBoxSendMessageRequest request, Action<EHealthBoxSendMessageRequestBuilder> callback)
        {
            var assertion = _sessionService.GetSession().Body.Response.Assertion;
            return SendMessage(request, callback, assertion);
        }

        public Task<SOAPEnvelope<EHealthBoxSendMessageResponseBody>> SendMessage(EHealthBoxSendMessageRequest request, Action<EHealthBoxSendMessageRequestBuilder> callback, SAMLAssertion assertion)
        {
            var builder = EHealthBoxSendMessageRequestBuilder.New();
            callback(builder);
            builder.Build();
            return SendMessage(builder.Build(), assertion);
        }

        public async Task<SOAPEnvelope<EHealthBoxSendMessageResponseBody>> SendMessage(EHealthBoxSendMessageRequest request, SAMLAssertion assertion)
        {
            var issueInstant = DateTime.UtcNow;
            var orgCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var soapRequest = SOAPRequestBuilder<EHealthBoxSendMessageRequestBody>.New(new EHealthBoxSendMessageRequestBody
            {
                Id = $"id-{Guid.NewGuid().ToString()}",
                Request = request
            })
                .AddTimestamp(issueInstant, issueInstant.AddHours(1))
                .AddSAMLAssertion(assertion)
                .AddReferenceToSAMLAssertion()
                .SignWithCertificate(orgCertificate)
                .Build();
            var httpResult = await _soapClient.Send(soapRequest, new Uri(_options.EhealthboxPublication), "urn:be:fgov:ehealth:ehbox:publication:protocol:v3:sendMessage");
            var xml = await httpResult.Content.ReadAsStringAsync();
            httpResult.EnsureSuccessStatusCode();
            var result = SOAPEnvelope<EHealthBoxSendMessageResponseBody>.Deserialize(xml);
            return result;
        }
        
        public Task<SOAPEnvelope<EHealthBoxDeleteMessageResponseBody>> DeleteMessage(EHealthBoxDeleteMessageRequest request)
        {
            var assertion = _sessionService.GetSession().Body.Response.Assertion;
            return DeleteMessage(request, assertion);
        }

        public async Task<SOAPEnvelope<EHealthBoxDeleteMessageResponseBody>> DeleteMessage(EHealthBoxDeleteMessageRequest request, SAMLAssertion assertion)
        {
            var issueInstant = DateTime.UtcNow;
            var orgCertificate = _keyStoreManager.GetOrgAuthCertificate();
            var soapRequest = SOAPRequestBuilder<EHealthBoxDeleteMessageRequestBody>.New(new EHealthBoxDeleteMessageRequestBody
            {
                Id = $"id-{Guid.NewGuid().ToString()}",
                Request = request
            })
                .AddTimestamp(issueInstant, issueInstant.AddHours(1))
                .AddSAMLAssertion(assertion)
                .AddReferenceToSAMLAssertion()
                .SignWithCertificate(orgCertificate)
                .Build();
            var httpResult = await _soapClient.Send(soapRequest, new Uri(_options.EHealthboxConsultation), "urn:be:fgov:ehealth:ehbox:consultation:protocol:v3:deleteMessage");
            var xml = await httpResult.Content.ReadAsStringAsync();
            httpResult.EnsureSuccessStatusCode();
            var result = SOAPEnvelope<EHealthBoxDeleteMessageResponseBody>.Deserialize(xml);
            return result;
        }
    }
}