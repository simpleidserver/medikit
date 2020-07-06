// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Medikit.Api.EHealth.Application.Domains
{
    [DebuggerDisplay("Code is {Code}")]
    public class KMEHRReferenceRecord : ICloneable
    {
        public KMEHRReferenceRecord()
        {
            Translations = new List<KMEHRReferenceRecordTranslation>();
        }

        public string Code { get; set; }
        public ICollection<KMEHRReferenceRecordTranslation> Translations { get; set; }

        public object Clone()
        {
            return new KMEHRReferenceRecord
            {
                Code = Code,
                Translations = Translations.Select(t => (KMEHRReferenceRecordTranslation)t.Clone()).ToList()
            };
        }
    }
}