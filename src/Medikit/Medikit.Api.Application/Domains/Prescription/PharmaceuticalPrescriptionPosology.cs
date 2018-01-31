// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Medikit.Api.Application.Domains
{
    public class PharmaceuticalPrescriptionPosology
    {
        public PharmaceuticalPrescriptionPosology(PosologyTypes type)
        {
            Type = type;
        }

        public PosologyTypes Type { get; private set; }
    }
}
