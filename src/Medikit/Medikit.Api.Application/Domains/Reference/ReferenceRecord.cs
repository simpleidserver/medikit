// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Medikit.Api.Application.Domains
{
    [DebuggerDisplay("Code is {Code}")]
    public class ReferenceRecord : ICloneable
    {
        public ReferenceRecord()
        {
            Translations = new List<ReferenceRecordTranslation>();
        }

        public string Code { get; set; }
        public ICollection<ReferenceRecordTranslation> Translations { get; set; }

        public object Clone()
        {
            return new ReferenceRecord
            {
                Code = Code,
                Translations = Translations.Select(t => (ReferenceRecordTranslation)t.Clone()).ToList()
            };
        }
    }
}