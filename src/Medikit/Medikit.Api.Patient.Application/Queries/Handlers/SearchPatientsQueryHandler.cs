// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application.Persistence;
using Medikit.Api.Patient.Application.Extensions;
using Medikit.Api.Patient.Application.Persistence;
using Medikit.Api.Patient.Application.Queries.Results;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Patient.Application.Queries.Handlers
{
    public class SearchPatientsQueryHandler : IRequestHandler<SearchPatientsQuery, PagedResult<GetPatientQueryResult>>
    {
        private readonly IPatientQueryRepository _patientQueryRepository;

        public SearchPatientsQueryHandler(IPatientQueryRepository patientQueryRepository)
        {
            _patientQueryRepository = patientQueryRepository;
        }

        public async Task<PagedResult<GetPatientQueryResult>> Handle(SearchPatientsQuery query, CancellationToken token)
        {
            var result = await _patientQueryRepository.Search(query, token);
            return new PagedResult<GetPatientQueryResult>
            {
                Count = result.Count,
                StartIndex = result.StartIndex,
                TotalLength = result.TotalLength,
                Content = result.Content.Select(_ => _.ToResult()).ToList()
            };
        }
    }
}
