﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medikit.Api.Common.Application.EvtStore.InMemory
{
    public class InMemoryAggregateSnapshotStore : IAggregateSnapshotStore
    {
        private ICollection<SnapshotElement<BaseAggregate>> _snapshots;

        public InMemoryAggregateSnapshotStore()
        {
            _snapshots = new List<SnapshotElement<BaseAggregate>>();
        }

        public Task<SnapshotElement<T>> GetLast<T>(string id) where T : BaseAggregate
        {
            lock(_snapshots)
            {
                var result = _snapshots.Where(s => s.Id == id).OrderByDescending(s => s.CreateDateTime).FirstOrDefault();
                if (result == null)
                {
                    return Task.FromResult((SnapshotElement<T>)null);
                }

                return Task.FromResult(Copy<T>(result));
            }
        }

        public Task<bool> Add(SnapshotElement<BaseAggregate> snapshot)
        {
            lock(_snapshots)
            {
                _snapshots.Add(snapshot);
                return Task.FromResult(true);
            }
        }

        public ICollection<SnapshotElement<T>> Query<T>() where T : BaseAggregate
        {
            return _snapshots.Cast<SnapshotElement<T>>().ToList();
        }

        private static SnapshotElement<T> Copy<T>(SnapshotElement<BaseAggregate> obj) where T : BaseAggregate
        {
            var copy = (T)(obj.Content).Clone();
            return new SnapshotElement<T>(obj.Start, obj.CreateDateTime, obj.Id, copy);
        }
    }
}
