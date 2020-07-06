// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Common.Application.Bus.InMemory
{
    public class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryMessageBroker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task Start()
        {
            return Task.CompletedTask;
        }

        public async Task Queue(string queueName, object message)
        {
            var genericType = message.GetType();
            var messageBrokerType = typeof(IMessageBrokerConsumerGeneric<>).MakeGenericType(genericType);
            var lst = (IEnumerable<object>)_serviceProvider.GetService(typeof(IEnumerable<>).MakeGenericType(messageBrokerType));
            var taskLst = new List<Task>();
            foreach (var r in lst)
            {
                taskLst.Add((Task)messageBrokerType.GetMethod("Handle").Invoke(r, new object[] { message, CancellationToken.None }));
            }

            await Task.WhenAll(taskLst);
        }

        public Task Stop()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
