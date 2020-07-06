// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Medicalfile.Application.Extensions;
using Medikit.Api.Medicalfile.Application.Resources;
using Medikit.Api.Medicalfile.Prescription.Prescription.Results;
using Medikit.Api.Patient.Application.Persistence;
using Medikit.EHealth.EHealthServices;
using Medikit.EHealth.EHealthServices.Parameters;
using Medikit.EHealth.EHealthServices.Results;
using Medikit.EHealth.Enums;
using Medikit.EHealth.Exceptions;
using Medikit.EHealth.SAML.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Prescription.Queries.Handlers
{
    public class GetPharmaceuticalPrescriptionQueryHandler : IRequestHandler<GetPharmaceuticalPrescriptionQuery, GetPharmaceuticalPrescriptionResult>
    {
        private readonly IEHealthPrescriptionService _prescriptionService;
        private readonly IEHealthAmpService _ampService;
        private readonly IPatientQueryRepository _patientQueryRepository;

        public GetPharmaceuticalPrescriptionQueryHandler(IEHealthPrescriptionService prescriptionService, IEHealthAmpService ampService, IPatientQueryRepository patientQueryRepository)
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

            var prescription = await _prescriptionService.GetPrescription(new GetPrescriptionParameter
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
                lst.Add(_ampService.SearchByCnkCode(DeliveryEnvironments.Public.Code, cnkCode, token));
            }

            var ampLst = await Task.WhenAll(lst);
            return prescription.ToResult(patient, ampLst.ToList());
        }
    }
}
