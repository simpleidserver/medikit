// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Persistence;
using Medikit.Api.Patient.Application.Domains;
using Medikit.Api.Patient.Application.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Patient.Application.Persistence
{
    public interface IPatientQueryRepository
    {
        Task<PatientAggregate> GetById(string id, CancellationToken token);
        Task<PatientAggregate> GetByNiss(string niss, CancellationToken token);
        Task<PagedResult<PatientAggregate>> Search(SearchPatientsQuery parameter, CancellationToken token);
    }
}
