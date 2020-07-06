// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;

namespace Medikit.Api.Common.Application.Bus
{
    public interface IMessageBroker : IDisposable
    {
        Task Queue(string queueName, object message);
    }
}