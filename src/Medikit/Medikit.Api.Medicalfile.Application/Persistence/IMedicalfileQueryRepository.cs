// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Persistence;
using Medikit.Api.Medicalfile.Application.Domains;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Persistence
{
    public interface IMedicalfileQueryRepository
    {
        Task<MedicalfileAggregate> Get(string id, CancellationToken token);
        Task<PagedResult<MedicalfileAggregate>> Search(SearchMedicalfileQuery parameter, CancellationToken token);
    }
}
