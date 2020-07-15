// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.EHealth.Application.Message.Commands;
using Medikit.Api.EHealth.Application.Message.Queries;
using Medikit.Api.EHealth.Application.Message.Queries.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Api.EHealth.Application.Message
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageResult>> SearchInboxMessages(GetMessagesQuery query);
        Task<IEnumerable<MessageResult>> SearchSentboxMessages(GetMessagesQuery query);
        Task<bool> DeleteInboxMessages(DeleteMessageCommand command);
        Task<bool> DeleteSentboxMessages(DeleteMessageCommand command);
    }
}
