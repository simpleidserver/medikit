// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Common.Application.Bus
{
    public interface IMessageBrokerConsumerGeneric<T> : IMessageBrokerConsumer where T : class
    {
        Task Handle(T message, CancellationToken token);
    }
}
