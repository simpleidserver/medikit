// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Infrastructure;
using Medikit.Api.Application.Infrastructure.Bus;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Extensions
{
    public static class MessageBrokerExtensions
    {
        public static Task QueueEvent(this IMessageBroker messageBroker, DomainEvent domainEvent, string queueName)
        {
            return messageBroker.Queue(queueName, domainEvent);
        }
    }
}
