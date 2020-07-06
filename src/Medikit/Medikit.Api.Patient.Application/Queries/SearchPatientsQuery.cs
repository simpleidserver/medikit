// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application.Persistence;
using Medikit.Api.Common.Application.Queries;
using Medikit.Api.Patient.Application.Queries.Results;

namespace Medikit.Api.Patient.Application.Queries
{
    public class SearchPatientsQuery : BaseSearchQuery, IRequest<PagedResult<GetPatientQueryResult>>
    {
        public SearchPatientsQuery()
        {
            StartIndex = 0;
            Count = 100;
        }

        public string Niss { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
