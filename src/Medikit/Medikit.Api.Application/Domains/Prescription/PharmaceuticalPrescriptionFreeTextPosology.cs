﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Medikit.Api.Application.Domains
{
    public class PharmaceuticalPrescriptionFreeTextPosology : PharmaceuticalPrescriptionPosology
    {
        public PharmaceuticalPrescriptionFreeTextPosology() : base(PosologyTypes.FreeText) { }

        public string Content { get; set; }
    }
}