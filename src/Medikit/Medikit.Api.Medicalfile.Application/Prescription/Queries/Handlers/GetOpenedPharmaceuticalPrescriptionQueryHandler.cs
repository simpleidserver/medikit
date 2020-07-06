// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Medicalfile.Application.Resources;
using Medikit.Api.Patient.Application.Exceptions;
using Medikit.Api.Patient.Application.Persistence;
using Medikit.EHealth.EHealthServices;
using Medikit.EHealth.EHealthServices.Parameters;
using Medikit.EHealth.Exceptions;
using Medikit.EHealth.SAML.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Prescription.Queries.Handlers
{
    public class GetOpenedPharmaceuticalPrescriptionQueryHandler : IRequestHandler<GetOpenedPharmaceuticalPrescriptionQuery, ICollection<string>>
    {
        private readonly IEHealthPrescriptionService _prescriptionService;
        private readonly IPatientQueryRepository _patientQueryRepository;

        public GetOpenedPharmaceuticalPrescriptionQueryHandler(IEHealthPrescriptionService prescriptionService, IPatientQueryRepository patientQueryRepository)
        {
            _prescriptionService = prescriptionService;
            _patientQueryRepository = patientQueryRepository;
        }

        public async Task<ICollection<string>> Handle(GetOpenedPharmaceuticalPrescriptionQuery query, CancellationToken token)
        {
            SAMLAssertion assertion;
            var patient = await _patientQueryRepository.GetByNiss(query.PatientNiss, token);
            if (patient == null)
            {
                throw new UnknownPatientException(query.PatientNiss, string.Format(Global.UnknownPatient, query.PatientNiss));
            }

            try
            {
                assertion = SAMLAssertion.Deserialize(query.AssertionToken);
            }
            catch
            {
                throw new BadAssertionTokenException(Global.BadAssertionToken);
            }

            return await _prescriptionService.GetOpenedPrescriptions(new GetOpenedPrescriptionsParameter
            {
                PatientNiss = query.PatientNiss,
                PageNumber = query.PageNumber,
                Assertion = assertion
            }, token);
        }
    }
}
