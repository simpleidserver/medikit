// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Metadata;
using Medikit.EHealth.Enums;
using System;
using System.Collections.Generic;

namespace Medikit.Api.Medicalfile.Prescription.Prescription.Results
{
    public class GetPharmaceuticalPrescriptionResult
    {
        public GetPharmaceuticalPrescriptionResult()
        {
            Medications = new List<MedicationResult>();
        }

        public string Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime EndExecutionDate { get; set; }
        public PrescriberResult Prescriber { get; set; }
        public PatientResult Patient { get; set; }
        public int PrescriptionType { get; set; }
        public ICollection<MedicationResult> Medications { get; set; }

        public class MedicationResult
        {
            public MedicationPackageResult MedicationPackage { get; set; }
            public PosologyResult Posology { get; set; }
            public string InstructionForPatient { get; set; }
            public string InstructionForReimbursement { get; set; }
        }

        public class MedicationPackageResult
        {
            public string PackageCode { get; set; }
            public ICollection<TranslationResult> Translations { get; set; }
        }

        public class PersonResult
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public DateTime Birthdate { get; set; }
        }

        public class PatientResult : PersonResult
        {
            public string Niss { get; set; }
        }

        public class PrescriberResult : PersonResult
        {
            public string INAMINumber { get; set; }
        }

        public class PosologyResult
        {
            public PosologyResult(string type)
            {
                Type = type;
            }

            public string Type { get; set; }
        }

        public class PosologyFreeTextResult : PosologyResult
        {
            public PosologyFreeTextResult() : base(PosologyTypes.FreeText.Code) { }

            public string Content { get; set; }
        }
    }
}
