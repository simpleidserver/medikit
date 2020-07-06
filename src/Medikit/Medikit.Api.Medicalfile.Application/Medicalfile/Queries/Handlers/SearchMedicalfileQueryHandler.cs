// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application.Persistence;
using Medikit.Api.Medicalfile.Application.Extensions;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries.Results;
using Medikit.Api.Medicalfile.Application.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Medicalfile.Queries.Handlers
{
    public class SearchMedicalfileQueryHandler : IRequestHandler<SearchMedicalfileQuery, PagedResult<GetMedicalfileResult>>
    {
        private readonly IMedicalfileQueryRepository _medicalfileQueryRepository;

        public SearchMedicalfileQueryHandler(IMedicalfileQueryRepository medicalfileQueryRepository)
        {
            _medicalfileQueryRepository = medicalfileQueryRepository;
        }

        public async Task<PagedResult<GetMedicalfileResult>> Handle(SearchMedicalfileQuery request, CancellationToken cancellationToken)
        {
            var result = await _medicalfileQueryRepository.Search(request, cancellationToken);
            return new PagedResult<GetMedicalfileResult>
            {
                Count = result.Count,
                StartIndex = result.StartIndex,
                TotalLength = result.TotalLength,
                Content = result.Content.Select(_ => _.ToResult()).ToList()
            };
        }
    }
}
