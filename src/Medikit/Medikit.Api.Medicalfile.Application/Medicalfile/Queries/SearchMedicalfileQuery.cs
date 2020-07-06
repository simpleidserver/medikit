// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application.Persistence;
using Medikit.Api.Common.Application.Queries;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries.Results;

namespace Medikit.Api.Medicalfile.Application.Medicalfile.Queries
{
    public class SearchMedicalfileQuery : BaseSearchQuery, IRequest<PagedResult<GetMedicalfileResult>>
    {
        public SearchMedicalfileQuery()
        {
            StartIndex = 0;
            Count = 100;
        }

        public string Niss { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
