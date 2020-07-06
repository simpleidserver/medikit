// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application.Persistence;
using Medikit.Api.Patient.Application.Commands;
using Medikit.Api.Patient.Application.Queries;
using Medikit.Api.Patient.Application.Queries.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Patient.Application
{
    public class PatientService : IPatientService
    {
        private readonly IMediator _mediator;

        public PatientService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<GetPatientQueryResult> GetPatientByNiss(GetPatientByNissQuery query, CancellationToken token)
        {
            return _mediator.Send(query, token);
        }

        public Task<string> AddPatient(AddPatientCommand command, CancellationToken token)
        {
            return _mediator.Send(command, token);
        }

        public Task<PagedResult<GetPatientQueryResult>> Search(SearchPatientsQuery query, CancellationToken token)
        {
            return _mediator.Send(query, token);
        }

        public Task<GetPatientQueryResult> GetPatientById(GetPatientByIdQuery query, CancellationToken token)
        {
            return _mediator.Send(query, token);
        }
    }
}
