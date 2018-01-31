// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Exceptions;
using Medikit.Api.Application.Persistence;
using Medikit.Api.Application.Prescriptions.Results;
using Medikit.Api.Application.Resources;
using Medikit.Api.Application.Services;
using Medikit.Api.Application.Services.Results;
using Medikit.EHealth.SAML.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Prescriptions.Queries.Handlers
{
    public class GetPharmaceuticalPrescriptionQueryHandler : IGetPharmaceuticalPrescriptionQueryHandler
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IAmpService _ampService;
        private readonly IPatientQueryRepository _patientQueryRepository;

        public GetPharmaceuticalPrescriptionQueryHandler(IPrescriptionService prescriptionService, IAmpService ampService, IPatientQueryRepository patientQueryRepository)
        {
            _prescriptionService = prescriptionService;
            _ampService = ampService;
            _patientQueryRepository = patientQueryRepository;
        }

        public async Task<GetPharmaceuticalPrescriptionResult> Handle(GetPharmaceuticalPrescriptionQuery query, CancellationToken token)
        {
            SAMLAssertion assertion = null;
            try
            {
                assertion = SAMLAssertion.Deserialize(query.AssertionToken);
            }
            catch
            {
                throw new BadAssertionTokenException(Global.BadAssertionToken);
            }

            var prescription = await _prescriptionService.GetPrescription(new Services.Parameters.GetPrescriptionParameter
            {
                PrescriptionId = query.PrescriptionId,
                Assertion = assertion
            }, token);
            if (prescription == null)
            {
                throw new UnknownPrescriptionException(query.PrescriptionId, string.Format(Global.UnknownPrescription, query.PrescriptionId));
            }

            var patient = await _patientQueryRepository.GetByNiss(prescription.PatientNiss, token);
            var cnkCodes = prescription.Medications.Select(m => m.PackageCode);
            var lst = new List<Task<AmpResult>>();
            foreach(var cnkCode in cnkCodes)
            {
                lst.Add(_ampService.SearchByCnkCode(DeliveryEnvironments.Public.Name, cnkCode, token));
            }

            var ampLst = await Task.WhenAll(lst);
            var result = new GetPharmaceuticalPrescriptionResult
            {
                Id = prescription.Id,
                CreateDateTime = prescription.CreateDateTime,
                EndExecutionDate = prescription.EndExecutionDate,
                PrescriptionType = (int)prescription.PrescriptionType,
                Prescriber = new GetPharmaceuticalPrescriptionPrescriberResult
                {
                    Birthdate = DateTime.UtcNow,
                    Firstname = "John",
                    Lastname = "Doe",
                    INAMINumber = "123456"
                },
                Patient = new GetPharmaceuticalPrescriptionPatientResult
                {
                    Birthdate = patient.BirthDate,
                    Firstname = patient.Firstname,
                    Lastname = patient.Lastname,
                    Niss = patient.NationalIdentityNumber
                },
                Medications = prescription.Medications.Select(_ => new GetPharmaceuticalPrescriptionMedication
                {
                    InstructionForPatient = _.InstructionForPatient,
                    InstructionForReimbursement = _.InstructionForReimbursement,
                    MedicationPackage = new GetPharmaceuticalPrescriptionPackage
                    {
                        PackageCode = _.PackageCode,
                        Translations = ampLst.SelectMany(_ => _.Names).Select(a => new Common.TranslationResult
                            {
                                Language = a.Language,
                                Value = a.Value
                            }).ToList()
                    },
                    Posology = _.Posology == null || _.Posology.Type.Id != PosologyTypes.FreeText.Id ? null : new GetPharmaceuticalPrescriptionPosologyFreeText
                    {
                        Content = ((PharmaceuticalPrescriptionFreeTextPosology)_.Posology).Content
                    }
                }).ToList()
            };
            return result;
        }
    }
}
