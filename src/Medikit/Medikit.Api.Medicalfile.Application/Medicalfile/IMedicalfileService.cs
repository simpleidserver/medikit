// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Persistence;
using Medikit.Api.Medicalfile.Application.Medicalfile.Commands;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Medicalfile
{
    public interface IMedicalfileService
    {
        Task<string> AddMedicalfile(AddMedicalfileCommand command, CancellationToken token);
        Task<GetMedicalfileResult> GetMedicalfile(string id, CancellationToken token);
        Task<PagedResult<GetMedicalfileResult>> SearchMedicalfiles(SearchMedicalfileQuery query, CancellationToken token);
    }
}
