// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Domains;
using System;

namespace Medikit.Api.Common.Application.EvtStore
{
    public class SnapshotElement<T> where T : BaseAggregate
    {
        public SnapshotElement() { }

        public SnapshotElement(long start, DateTime createDateTime, string id, T content)
        {
            Start = start;
            CreateDateTime = createDateTime;
            Id = id;
            Content = content;
        }

        public long Start { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Id { get; set; }
        public T Content { get; set; }
    }
}
