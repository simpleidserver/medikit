// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace Medikit.Api.EHealth.Application.KMEHRReference.Queries.Results
{
    public class KMEHRReferenceRecordResult
    {
        public KMEHRReferenceRecordResult()
        {
            Translations = new List<KMEHRReferenceRecordTranslationResult>();
        }

        public string Code { get; set; }
        public ICollection<KMEHRReferenceRecordTranslationResult> Translations { get; set; }
    }
}
