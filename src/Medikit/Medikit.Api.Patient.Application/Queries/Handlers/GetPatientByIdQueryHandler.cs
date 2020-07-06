// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Patient.Application.Exceptions;
using Medikit.Api.Patient.Application.Extensions;
using Medikit.Api.Patient.Application.Persistence;
using Medikit.Api.Patient.Application.Queries.Results;
using Medikit.Api.Patient.Application.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Patient.Application.Queries.Handlers
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, GetPatientQueryResult>
    {
        private readonly IPatientQueryRepository _patientQueryRepository;

        public GetPatientByIdQueryHandler(IPatientQueryRepository patientQueryRepository)
        {
            _patientQueryRepository = patientQueryRepository;
        }

        public async Task<GetPatientQueryResult> Handle(GetPatientByIdQuery query, CancellationToken token)
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
