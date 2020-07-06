// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Enums;

namespace Medikit.EHealth.EHealthServices.Results
{
    public class PharmaceuticalPrescriptionPosologyResult
    {
        public PharmaceuticalPrescriptionPosologyResult(PosologyTypes type)
        {
            Type = type;
        }

        public PosologyTypes Type { get; private set; }
    }
}
