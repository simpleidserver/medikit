// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.EHealth.Application.Extensions;
using Medikit.Api.EHealth.Application.Message.Queries.Results;
using Medikit.Api.EHealth.Application.Resources;
using Medikit.EHealth.Exceptions;
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.Services.EHealthBox;
using Medikit.EHealth.Services.EHealthBox.Request;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.EHealth.Application.Message.Queries.Handlers
{
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, IEnumerable<MessageResult>>
    {
        private readonly IEHealthBoxService _ehealthBoxService;

        public GetMessagesQueryHandler(IEHealthBoxService eHealthBoxService)
        {
            _ehealthBoxService = eHealthBoxService;
        }

        public async Task<IEnumerable<MessageResult>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            SAMLAssertion assertion;
            try
            {
                assertion = SAMLAssertion.Deserialize(request.AssertionToken);
            }
            catch
            {
                throw new BadAssertionTokenException(Global.BadAssertionToken);
            }

            var messagesResult = await _ehealthBoxService.GetMessagesList(new EHealthBoxGetMessagesListRequest
            {
                StartIndex = request.StartIndex,
                EndIndex = request.EndIndex,
                Source = request.Source
            }, assertion);
            return messagesResult.Body.GetMessagesListResponse.MessageLst.Select(_ => _.ToResult());
        }
    }
}
