// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Common;
using System.Collections.Generic;

namespace Medikit.Api.Application.Metadata
{
    public class MetadataRecord
    {
        public MetadataRecord()
        {
            Children = new Dictionary<string, MetadataRecord>();
            Translations = new List<TranslationResult>();
        }

        public Dictionary<string, MetadataRecord> Children { get; set; }
        public ICollection<TranslationResult> Translations { get; set; }
    }
}
