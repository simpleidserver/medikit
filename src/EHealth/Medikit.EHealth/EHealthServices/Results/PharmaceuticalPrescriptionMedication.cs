﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Medikit.EHealth.EHealthServices.Results
{
    public class PharmaceuticalPrescriptionMedication
    {
        public string PackageCode { get; set; }
        public string InstructionForPatient { get; set; }
        public string InstructionForReimbursement { get; set; }
        public PharmaceuticalPrescriptionPosologyResult Posology { get; set; }
    }
}
