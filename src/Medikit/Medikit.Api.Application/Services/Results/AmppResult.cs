// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Common;
using System.Collections.Generic;

namespace Medikit.Api.Application.Services.Results
{
    public class AmppResult
    {
        public ICollection<TranslationResult> PrescriptionNames { get; set; }
        public ICollection<DmppResult> DeliveryMethods { get; set; }
    }
}
