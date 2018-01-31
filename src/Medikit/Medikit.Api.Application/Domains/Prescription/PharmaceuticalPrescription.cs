// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;

namespace Medikit.Api.Application.Domains
{
    public class PharmaceuticalPrescription
    {
        public PharmaceuticalPrescription()
        {
            Medications = new List<PharmaceuticalPrescriptionMedication>();
        }

        public string Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime EndExecutionDate { get; set; }
        public string PatientNiss { get; set; }
        public PrescriptionTypes PrescriptionType { get; set; }
        public ICollection<PharmaceuticalPrescriptionMedication> Medications { get; set; }
    }
}
