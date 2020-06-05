// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Common;
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Patient.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Persistence
{
    public interface IPatientQueryRepository
    {
        Task<PatientAggregate> GetByNiss(string niss, CancellationToken token);
        Task<PagedResult<PatientAggregate>> Search(SearchPatientsQuery parameter, CancellationToken token);
    }
}
