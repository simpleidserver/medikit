// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;

namespace Medikit.Api.Application.Prescriptions.Results
{
    public class GetPharmaceuticalPrescriptionResult
    {
        public GetPharmaceuticalPrescriptionResult()
        {
            Medications = new List<GetPharmaceuticalPrescriptionMedication>();
        }

        public string Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime EndExecutionDate { get; set; }
        public GetPharmaceuticalPrescriptionPrescriberResult Prescriber { get; set; }
        public GetPharmaceuticalPrescriptionPatientResult Patient { get; set; }
        public int PrescriptionType { get; set; }
        public ICollection<GetPharmaceuticalPrescriptionMedication> Medications { get; set; }
    }
}
