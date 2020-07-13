// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.EHealth.Application.Message.Queries;
using Medikit.Api.EHealth.Application.Message.Queries.Results;
using Medikit.EHealth.Services.EHealthBox;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Api.EHealth.Application.Message
{
    public class MessageService : IMessageService
    {
        private readonly IMediator _mediator;

        public MessageService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<IEnumerable<MessageResult>> SearchInboxMessages(GetMessagesQuery query)
        {
            query.Source = EHealthBoxSources.INBOX;
            return _mediator.Send(query);
        }

        public Task<IEnumerable<MessageResult>> SearchSentboxMessages(GetMessagesQuery query)
        {
            query.Source = EHealthBoxSources.SENTBOX;
            return _mediator.Send(query);
        }
    }
}
