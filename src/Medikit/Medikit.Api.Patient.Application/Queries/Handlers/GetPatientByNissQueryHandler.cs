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
    public class GetPatientByNissQueryHandler : IRequestHandler<GetPatientByNissQuery, GetPatientQueryResult>
    {
        private readonly IPatientQueryRepository _patientQueryRepository;

        public GetPatientByNissQueryHandler(IPatientQueryRepository patientQueryRepository)
        {
            _patientQueryRepository = patientQueryRepository;
        }

        public async Task<GetPatientQueryResult> Handle(GetPatientByNissQuery query, CancellationToken token)
        {
            var result = await _patientQueryRepository.GetByNiss(query.Niss, token);
            if (result == null)
            {
                throw new UnknownPatientException(query.Niss, string.Format(Global.UnknownPatient, query.Niss));
            }

            return result.ToResult();
        }
    }
}
