// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application.Persistence;
using Medikit.Api.Medicalfile.Application.Medicalfile.Commands;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Medicalfile
{
    public class MedicalfileService : IMedicalfileService
    {
        private readonly IMediator _mediator;

        public MedicalfileService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<string> AddMedicalfile(AddMedicalfileCommand command, CancellationToken token)
        {
            return _mediator.Send(command, token);
        }

        public Task<GetMedicalfileResult> GetMedicalfile(string id, CancellationToken token)
        {
            return _mediator.Send(new GetMedicalfileQuery { Id = id }, token);
        }

        public Task<PagedResult<GetMedicalfileResult>> SearchMedicalfiles(SearchMedicalfileQuery query, CancellationToken token)
        {
            return _mediator.Send(query, token);
        }
    }
}
