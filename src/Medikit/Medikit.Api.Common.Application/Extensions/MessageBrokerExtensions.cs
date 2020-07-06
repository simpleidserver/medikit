// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Bus;
using Medikit.Api.Common.Application.Domains;
using System.Threading.Tasks;

namespace Medikit.Api.Common.Application.Extensions
{
    public static class MessageBrokerExtensions
    {
        public static Task QueueEvent(this IMessageBroker messageBroker, DomainEvent domainEvent, string queueName)
        {
            return messageBroker.Queue(queueName, domainEvent);
        }
    }
}
