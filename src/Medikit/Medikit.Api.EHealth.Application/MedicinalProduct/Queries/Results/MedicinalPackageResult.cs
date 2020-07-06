// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Metadata;
using System.Collections.Generic;

namespace Medikit.Api.EHealth.Application.MedicinalProduct.Queries.Results
{
    public class MedicinalPackageResult
    {
        public string Code { get; set; }
        public double Price { get; set; }
        public bool Reimbursable { get; set; }
        public ICollection<TranslationResult> Names { get; set; }
        public ICollection<TranslationResult> LeafletUrlLst { get; set; }
        public ICollection<TranslationResult> SpcUrlLst { get; set; }
        public ICollection<TranslationResult> CrmUrlLst { get; set; }
    }
}
