// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Common;
using Medikit.Api.Application.Patient.Commands;
using Medikit.Api.Application.Patient.Queries;
using Medikit.Api.Application.Patient.Queries.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Patient
{
    public interface IPatientService
    {
        Task<PatientResult> GetPatientByNiss(GetPatientByNissQuery query, CancellationToken token);
        Task<string> AddPatient(AddPatientCommand command, CancellationToken token);
        Task<PagedResult<PatientResult>> Search(SearchPatientsQuery query, CancellationToken token);
    }
}
