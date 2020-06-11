// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using System;
using System.Collections.Generic;

namespace Medikit.Api.Application.Prescriptions.Commands
{
    public class AddPharmaceuticalPrescriptionCommand
    {
        public AddPharmaceuticalPrescriptionCommand()
        {
            Medications = new List<AddPharmaceuticalPrescriptionMedication>();
        }

        public string AssertionToken { get; set; }
        public string PatientNiss { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? ExpirationDateTime { get; set; }
        public PrescriptionTypes PrescriptionType { get; set; }
        public ICollection<AddPharmaceuticalPrescriptionMedication> Medications { get; set; }
    }
}
