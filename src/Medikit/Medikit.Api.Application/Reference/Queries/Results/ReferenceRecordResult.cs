// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Medikit.Api.Application.Reference.Queries.Results
{
    public class ReferenceRecordResult
    {
        public ReferenceRecordResult()
        {
            Translations = new List<ReferenceRecordTranslationResult>();
        }

        public string Code { get; set; }
        public ICollection<ReferenceRecordTranslationResult> Translations { get; set; }
    }
}
