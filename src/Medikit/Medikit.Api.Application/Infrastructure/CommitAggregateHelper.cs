// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Extensions;
using Medikit.Api.Application.Infrastructure.Bus;
using Medikit.Api.Application.Infrastructure.EvtStore;
using Microsoft.Extensions.Options;
using NEventStore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Infrastructure
{
    public class CommitAggregateHelper : ICommitAggregateHelper
    {
        private readonly IStoreEvents _storeEvents;
        private readonly IMessageBroker _messageBroker;
        private readonly IAggregateSnapshotStore _aggregateSnapshotStore;
        private readonly MedikitServerOptions _serverOptions;

        public CommitAggregateHelper(IStoreEvents storeEvents, IMessageBroker messageBroker, IAggregateSnapshotStore aggregateSnapshotStore, IOptions<MedikitServerOptions> options)
        {
            _storeEvents = storeEvents;
            _messageBroker = messageBroker;
            _aggregateSnapshotStore = aggregateSnapshotStore;
            _serverOptions = options.Value;
        }

        public async Task Commit<T>(T aggregate, string streamName, string queueName) where T : BaseAggregate
        {
            using (var evtStream = _storeEvents.OpenStream(streamName, streamName, int.MinValue, int.MaxValue))
            {
                lock (aggregate.DomainEvents)
                {
                    foreach (var domainEvent in aggregate.DomainEvents)
                    {
                        evtStream.Add(new EventMessage { Body = domainEvent });
                    }
                }

                evtStream.CommitChanges(Guid.NewGuid());
            }

            foreach (var evt in aggregate.DomainEvents)
            {
                await _messageBroker.QueueEvent(evt, queueName);
            }

            if (((aggregate.Version - 1) % _serverOptions.SnapshotFrequency) == 0)
            {
                await _aggregateSnapshotStore.Add(new SnapshotElement<BaseAggregate>(0, DateTime.UtcNow, streamName, aggregate));
            }
        }

        public async Task Commit<T>(T aggregate, ICollection<DomainEvent> evts, int aggregateVersion, string streamName, string queueName) where T : BaseAggregate
        {
            using (var evtStream = _storeEvents.OpenStream(streamName, streamName, int.MinValue, int.MaxValue))
            {
                foreach (var domainEvent in evts)
                {
                    evtStream.Add(new EventMessage { Body = domainEvent });
                }

                evtStream.CommitChanges(Guid.NewGuid());
            }

            foreach (var evt in evts)
            {
                await _messageBroker.QueueEvent(evt, queueName);
            }

            if (((aggregateVersion - 1) % _serverOptions.SnapshotFrequency) == 0)
            {
                await _aggregateSnapshotStore.Add(new SnapshotElement<BaseAggregate>(0, DateTime.UtcNow, streamName, aggregate));
            }
        }
    }
}
