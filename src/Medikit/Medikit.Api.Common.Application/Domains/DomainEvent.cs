// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Medikit.Api.Common.Application.Domains
{
    public class DomainEvent
    {
        public DomainEvent(string id, string aggregateId, int version)
        {
            Id = id;
            AggregateId = aggregateId;
            Version = version;
        }

        public string Id { get; set; }
        public string AggregateId { get; set; }
        public int Version { get; set; }
    }
}
