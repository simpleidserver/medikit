// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Medikit.Api.Application.Common;
using System.Collections.Generic;

namespace Medikit.Api.Application.MedicinalProduct.Queries.Results
{
    public class MedicinalPackageResult
    {
        public ICollection<TranslationResult> PrescriptionNames { get; set; }
        public ICollection<MedicinalDeliveryMethod> DeliveryMethods { get; set; }
    }
}
