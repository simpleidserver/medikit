// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Common;
using Medikit.Api.Application.Extensions;
using Medikit.Api.Application.Patient.Queries.Results;
using Medikit.Api.Application.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Patient.Queries.Handlers
{
    public class SearchPatientsQueryHandler : ISearchPatientsQueryHandler
    {
        private readonly IPatientQueryRepository _patientQueryRepository;

        public SearchPatientsQueryHandler(IPatientQueryRepository patientQueryRepository)
        {
            _patientQueryRepository = patientQueryRepository;
        }

        public async Task<PagedResult<PatientResult>> Handle(SearchPatientsQuery query, CancellationToken token)
        {
            var result = await _patientQueryRepository.Search(query, token);
            return new PagedResult<PatientResult>
            {
                Count = result.Count,
                StartIndex = result.StartIndex,
                TotalLength = result.TotalLength,
                Content = result.Content.Select(_ => _.ToResult()).ToList()
            };
        }
    }
}
