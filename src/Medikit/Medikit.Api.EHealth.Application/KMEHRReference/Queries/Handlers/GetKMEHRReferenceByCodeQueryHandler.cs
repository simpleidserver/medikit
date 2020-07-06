// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.EHealth.Application.KMEHRReference.Exceptions;
using Medikit.Api.EHealth.Application.KMEHRReference.Queries.Results;
using Medikit.Api.EHealth.Application.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.EHealth.Application.KMEHRReference.Queries.Handlers
{
    public class GetKMEHRReferenceByCodeQueryHandler : IRequestHandler<GetKMEHRReferenceByCodeQuery, KMEHRReferenceTableResult>
    {
        private readonly IKMEHRReferenceTableQueryRepository _referenceTableQueryRepository;

        public GetKMEHRReferenceByCodeQueryHandler(IKMEHRReferenceTableQueryRepository referenceTableQueryRepository)
        {
            _referenceTableQueryRepository = referenceTableQueryRepository;
        }

        public async Task<KMEHRReferenceTableResult> Handle(GetKMEHRReferenceByCodeQuery query, CancellationToken token)
        {
            var result = await _referenceTableQueryRepository.GetByCode(query.Code, query.Language);
            if (result == null)
            {
                throw new UnknownKMEHRReferenceTableException(query.Code);
            }

            return new KMEHRReferenceTableResult
            {
                Code = result.Code,
                Name = result.Name,
                PublishedDateTime = result.PublishedDateTime,
                Version = result.Version,
                Content = result.Content.Select(c => new KMEHRReferenceRecordResult
                {
                    Code = c.Code,
                    Translations = c.Translations.Select(t => new KMEHRReferenceRecordTranslationResult
                    {
                        Language = t.Language,
                        Value = t.Value
                    }).ToList()
                }).ToList()
            };
        }
    }
}
