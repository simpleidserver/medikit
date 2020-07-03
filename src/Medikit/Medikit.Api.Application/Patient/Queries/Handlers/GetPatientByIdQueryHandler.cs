// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Exceptions;
using Medikit.Api.Application.Extensions;
using Medikit.Api.Application.Patient.Queries.Results;
using Medikit.Api.Application.Persistence;
using Medikit.Api.Application.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Patient.Queries.Handlers
{
    public class GetPatientByIdQueryHandler : IGetPatientByIdQueryHandler
    {
        private readonly IPatientQueryRepository _patientQueryRepository;

        public GetPatientByIdQueryHandler(IPatientQueryRepository patientQueryRepository)
        {
            _patientQueryRepository = patientQueryRepository;
        }

        public async Task<PatientResult> Handle(GetPatientByIdQuery query, CancellationToken token)
        {
            var patient = await _patientQueryRepository.GetById(query.Id, token);
            if (patient == null)
            {
                throw new UnknownPatientException(query.Id, string.Format(Global.UnknownPatient, query.Id));
            }

            return patient.ToResult();
        }
    }
}
