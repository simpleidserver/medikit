// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Medikit.Api.Application.Domains;
using System;

namespace Medikit.Api.Application.Prescriptions.Commands
{
    public class AddPharmaceuticalPrescriptionMedication
    {
        public string PackageCode { get; set; }
        public string InstructionForPatient { get; set; }
        public string InstructionForReimbursement { get; set; }
        public DateTime? BeginMoment { get; set; }
        public PharmaceuticalPrescriptionPosology Posology { get; set; }
    }
}