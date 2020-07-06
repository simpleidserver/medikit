// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Metadata;
using Medikit.Api.Medicalfile.Application.Domains;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries.Results;
using Medikit.Api.Medicalfile.Prescription.Prescription.Results;
using Medikit.Api.Patient.Application.Domains;
using Medikit.EHealth.EHealthServices.Results;
using Medikit.EHealth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Medikit.Api.Medicalfile.Application.Extensions
{
    public static class ResultExtensions
    {
        public static GetMedicalfileResult ToResult(this MedicalfileAggregate medicalFile)
        {
            return new GetMedicalfileResult
            {
                CreateDateTime = medicalFile.CreateDateTime,
                Id = medicalFile.Id,
                PatientFirstname = medicalFile.PatientFirstname,
                PatientId = medicalFile.PatientId,
                PatientLastname = medicalFile.PatientLastname,
                PatientNiss = medicalFile.PatientNiss,
                UpdateDateTime = medicalFile.UpdateDateTime
            };
        }

        public static GetPharmaceuticalPrescriptionResult ToResult(this PharmaceuticalPrescriptionResult prescription, PatientAggregate patient, List<AmpResult> ampLst)
        {
            return new GetPharmaceuticalPrescriptionResult
            {
                Id = prescription.Id,
                CreateDateTime = prescription.CreateDateTime,
                EndExecutionDate = prescription.EndExecutionDate,
                PrescriptionType = (int)prescription.PrescriptionType,
                Prescriber = new GetPharmaceuticalPrescriptionResult.PrescriberResult
                {
                    Birthdate = DateTime.UtcNow,
                    Firstname = "John",
                    Lastname = "Doe",
                    INAMINumber = "123456"
                },
                Patient = new GetPharmaceuticalPrescriptionResult.PatientResult
                {
                    Birthdate = patient.BirthDate,
                    Firstname = patient.Firstname,
                    Lastname = patient.Lastname,
                    Niss = patient.NationalIdentityNumber
                },
                Medications = prescription.Medications.Select(_ => new GetPharmaceuticalPrescriptionResult.MedicationResult
                {
                    InstructionForPatient = _.InstructionForPatient,
                    InstructionForReimbursement = _.InstructionForReimbursement,
                    MedicationPackage = new GetPharmaceuticalPrescriptionResult.MedicationPackageResult
                    {
                        PackageCode = _.PackageCode,
                        Translations = ampLst.SelectMany(_ => _.Names).Select(a => new TranslationResult
                        {
                            Language = a.Language,
                            Value = a.Value
                        }).ToList()
                    },
                    Posology = _.Posology == null || _.Posology.Type.Code != PosologyTypes.FreeText.Code ? null : new GetPharmaceuticalPrescriptionResult.PosologyFreeTextResult
                    {
                        Content = ((PharmaceuticalPrescriptionFreeTextPosologyResult)_.Posology).Content
                    }
                }).ToList()
            };
        }
    }
}
