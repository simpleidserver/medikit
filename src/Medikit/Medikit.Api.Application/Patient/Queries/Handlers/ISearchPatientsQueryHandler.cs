﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Common;
using Medikit.Api.Application.Patient.Queries.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Patient.Queries.Handlers
{
    public interface ISearchPatientsQueryHandler
    {
        Task<PagedResult<PatientResult>> Handle(SearchPatientsQuery query, CancellationToken token);
    }
}