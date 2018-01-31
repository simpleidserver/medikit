// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Infrastructure.EvtStore
{
    public interface IEventStoreRepository
    {
        Task<T> GetLastAggregate<T>(string id, string streamName) where T : BaseAggregate;
        Task<T> GetLastAggregate<T>(string id, string streamName, IEnumerable<DomainEvent> domainEvents) where T : BaseAggregate;
        Task<IEnumerable<DomainEvent>> GetLastDomainEvents<T>(string id, string streamName) where T : BaseAggregate;
    }
}
