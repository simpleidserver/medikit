// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.Services.EHealthBox.Request;
using Medikit.EHealth.Services.EHealthBox.Response;
using Medikit.EHealth.SOAP.DTOs;
using System;
using System.Threading.Tasks;

namespace Medikit.EHealth.Services.EHealthBox
{
    public interface IEHealthBoxService
    {
        Task<SOAPEnvelope<EHealthBoxGetInfoResponseBody>> GetBoxInfo(EHealthBoxGetInfoRequest request);
        Task<SOAPEnvelope<EHealthBoxGetInfoResponseBody>> GetBoxInfo(EHealthBoxGetInfoRequest request, SAMLAssertion assertion);
        Task<SOAPEnvelope<EHealthBoxGetMessagesListResponseBody>> GetMessagesList(EHealthBoxGetMessagesListRequest request);
        Task<SOAPEnvelope<EHealthBoxGetMessagesListResponseBody>> GetMessagesList(EHealthBoxGetMessagesListRequest request, SAMLAssertion assertion);
        Task<SOAPEnvelope<EHealthBoxSendMessageResponseBody>> SendMessage(EHealthBoxSendMessageRequest request);
        Task<SOAPEnvelope<EHealthBoxSendMessageResponseBody>> SendMessage(EHealthBoxSendMessageRequest request, SAMLAssertion assertion);
        Task<SOAPEnvelope<EHealthBoxSendMessageResponseBody>> SendMessage(EHealthBoxSendMessageRequest request, Action<EHealthBoxSendMessageRequestBuilder> callback);
        Task<SOAPEnvelope<EHealthBoxSendMessageResponseBody>> SendMessage(EHealthBoxSendMessageRequest request, Action<EHealthBoxSendMessageRequestBuilder> callback, SAMLAssertion assertion);
        Task<SOAPEnvelope<EHealthBoxDeleteMessageResponseBody>> DeleteMessage(EHealthBoxDeleteMessageRequest request);
        Task<SOAPEnvelope<EHealthBoxDeleteMessageResponseBody>> DeleteMessage(EHealthBoxDeleteMessageRequest request, SAMLAssertion assertion);
    }
}
