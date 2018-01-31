// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Medikit.Api.Application.Prescriptions.Results
{
    public class GetPharmaceuticalPrescriptionMedication
    {
        public GetPharmaceuticalPrescriptionPackage MedicationPackage { get; set; }
        public GetPharmaceuticalPrescriptionPosology Posology { get; set; }
        public string InstructionForPatient { get; set; }
        public string InstructionForReimbursement { get; set; }
    }
}
