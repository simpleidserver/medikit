// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;

namespace Medikit.Api.Common.Application.Domains
{
    public abstract class BaseAggregate : ICloneable
    {
        public BaseAggregate()
        {
            DomainEvents = new List<DomainEvent>();
        }

        public string Id { get; set; }
        public int Version { get; set; }
        public ICollection<DomainEvent> DomainEvents { get; protected set; }

        public abstract object Clone();
        public abstract void Handle(dynamic obj);
    }
}
