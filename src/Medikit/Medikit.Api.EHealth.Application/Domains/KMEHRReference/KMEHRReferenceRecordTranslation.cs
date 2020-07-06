// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Diagnostics;

namespace Medikit.Api.EHealth.Application.Domains
{
    [DebuggerDisplay("Translation ({Language}) = {Value}")]
    public class KMEHRReferenceRecordTranslation : ICloneable
    {
        public string Language { get; set; }
        public string Value { get; set; }

        public object Clone()
        {
            return new KMEHRReferenceRecordTranslation
            {
                Language = Language,
                Value = Value
            };
        }
    }
}
