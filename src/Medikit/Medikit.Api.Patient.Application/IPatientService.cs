// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Persistence;
using Medikit.Api.Patient.Application.Commands;
using Medikit.Api.Patient.Application.Queries;
using Medikit.Api.Patient.Application.Queries.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Patient.Application
{
    public interface IPatientService
    {
        Task<GetPatientQueryResult> GetPatientByNiss(GetPatientByNissQuery query, CancellationToken token);
        Task<string> AddPatient(AddPatientCommand command, CancellationToken token);
        Task<PagedResult<GetPatientQueryResult>> Search(SearchPatientsQuery query, CancellationToken token);
        Task<GetPatientQueryResult> GetPatientById(GetPatientByIdQuery query, CancellationToken token);
    }
}
