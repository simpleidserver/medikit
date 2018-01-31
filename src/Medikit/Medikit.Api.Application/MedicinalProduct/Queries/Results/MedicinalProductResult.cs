// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Common;
using System.Collections.Generic;

namespace Medikit.Api.Application.MedicinalProduct.Queries.Results
{
    public class MedicinalProductResult
    {
        public string Code { get; set; }
        public string OfficialName { get; set; }
        public ICollection<TranslationResult> Names { get; set; }
        public ICollection<MedicinalPackageResult> Packages { get; set; }
    }
}
