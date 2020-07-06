// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
namespace Medikit.Api.EHealth.Application.KMEHRReference.Queries.Results
{
    public class KMEHRReferenceTableResult
    {
        public KMEHRReferenceTableResult()
        {
            Content = new List<KMEHRReferenceRecordResult>();
        }

        public string Code { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public DateTime PublishedDateTime { get; set; }
        public ICollection<KMEHRReferenceRecordResult> Content { get; set; }
    }
}
