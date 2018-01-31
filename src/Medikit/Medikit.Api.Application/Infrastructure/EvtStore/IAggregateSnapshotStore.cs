// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Infrastructure.EvtStore
{
    public interface IAggregateSnapshotStore 
    {
        Task<SnapshotElement<T>> GetLast<T>(string id) where T : BaseAggregate;
        Task<bool> Add(SnapshotElement<BaseAggregate> snapshot);
        ICollection<SnapshotElement<T>> Query<T>() where T : BaseAggregate;
    }
}
