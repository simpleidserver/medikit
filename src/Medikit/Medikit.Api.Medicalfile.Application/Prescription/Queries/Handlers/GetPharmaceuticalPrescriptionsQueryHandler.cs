// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Medicalfile.Application.Persistence;
using Medikit.Api.Medicalfile.Application.Prescription.Results;
using Medikit.Api.Medicalfile.Application.Resources;
using Medikit.EHealth.EHealthServices;
using Medikit.EHealth.EHealthServices.Parameters;
using Medikit.EHealth.Exceptions;
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.Services.Recipe.Request;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Prescription.Queries.Handlers
{
    public class GetPharmaceuticalPrescriptionsQueryHandler : IRequestHandler<GetPharmaceuticalPrescriptionsQuery, SearchPharmaceuticalPrescriptionResult>
    {
        private readonly IEHealthPrescriptionService _prescriptionService;
        private readonly IMedicalfileQueryRepository _medicalFileQueryRepository;

        public GetPharmaceuticalPrescriptionsQueryHandler(IEHealthPrescriptionService prescriptionService, IMedicalfileQueryRepository medicalfileQueryRepository)
        {
            _prescriptionService = prescriptionService;
            _medicalFileQueryRepository = medicalfileQueryRepository;
        }

        public async Task<SearchPharmaceuticalPrescriptionResult> Handle(GetPharmaceuticalPrescriptionsQuery query, CancellationToken token)
        {
            SAMLAssertion assertion;
            var medicalfile = await _medicalFileQueryRepository.Get(query.MedicalfileId, token);
            if (medicalfile == null)
            {
                throw new UnknownPrescriptionException(query.MedicalfileId, string.Format(Global.UnknownMedicalFile, query.MedicalfileId));
            }

            try
            {
                assertion = SAMLAssertion.Deserialize(query.AssertionToken);
            }
            catch
            {
                throw new BadAssertionTokenException(Global.BadAssertionToken);
            }

            var result = await _prescriptionService.GetHistoryPrescriptions(new GetPrescriptionsParameter
            {
                PatientNiss = medicalfile.PatientNiss,
                Page = new Page
                {
                    PageNumber = 0
                },
                Assertion = assertion
            }, token);
            return new SearchPharmaceuticalPrescriptionResult
            {
                HasMoreResults = result.HasMoreResults,
                Prescriptions = result.PrescriptionHistories.Select(_ => new SearchPharmaceuticalPrescriptionResult.PharmaceuticalPrescriptionResult
                {
                    RID = _.Rid,
                    Status = _.PrescriptionStatus
                }).ToList()
            };
        }
    }
}
