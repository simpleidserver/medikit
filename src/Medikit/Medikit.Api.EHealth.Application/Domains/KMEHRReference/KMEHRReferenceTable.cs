// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Medikit.Api.EHealth.Application.Domains
{
    public class KMEHRReferenceTable : ICloneable
    {
        public KMEHRReferenceTable()
        {
            Content = new List<KMEHRReferenceRecord>();
        }

        public string Code { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public DateTime PublishedDateTime { get; set; }
        public ICollection<KMEHRReferenceRecord> Content { get; set; }

        public object Clone()
        {
            return new KMEHRReferenceTable
            {
                Code = Code,
                Version = Version,
                Status = Status,
                Name = Name,
                PublishedDateTime = PublishedDateTime,
                Content = Content.Select(c => (KMEHRReferenceRecord)c.Clone()).ToList()
            };
        }
    }
}
