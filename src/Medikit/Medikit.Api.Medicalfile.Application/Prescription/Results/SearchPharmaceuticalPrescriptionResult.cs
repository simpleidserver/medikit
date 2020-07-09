// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace Medikit.Api.Medicalfile.Application.Prescription.Results
{
    public class SearchPharmaceuticalPrescriptionResult
    {
        public SearchPharmaceuticalPrescriptionResult()
        {
            Prescriptions = new List<PharmaceuticalPrescriptionResult>();
        }

        public ICollection<PharmaceuticalPrescriptionResult> Prescriptions { get; set; }
        public bool HasMoreResults { get; set; }

        public class PharmaceuticalPrescriptionResult
        {
            public string RID { get; set; }
            public string Status { get; set; }
        }
    }
}
