// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Persistence;
using Medikit.Api.Application.Reference.Exceptions;
using Medikit.Api.Application.Reference.Queries.Results;
using System.Linq;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Reference.Queries.Handlers
{
    public class GetReferenceByCodeQueryHandler : IGetReferenceByCodeQueryHandler
    {
        private readonly IReferenceTableQueryRepository _referenceTableQueryRepository;

        public GetReferenceByCodeQueryHandler(IReferenceTableQueryRepository referenceTableQueryRepository)
        {
            _referenceTableQueryRepository = referenceTableQueryRepository;
        }

        public async Task<ReferenceTableResult> Handle(GetReferenceByCodeQuery query)
        {
            var result = await _referenceTableQueryRepository.GetByCode(query.Code, query.Language);
            if (result == null)
            {
                throw new UnknownReferenceTableException(query.Code);
            }

            return new ReferenceTableResult
            {
                Code = result.Code,
                Name = result.Name,
                PublishedDateTime = result.PublishedDateTime,
                Version = result.Version,
                Content = result.Content.Select(c => new ReferenceRecordResult
                {
                    Code = c.Code,
                    Translations = c.Translations.Select(t => new ReferenceRecordTranslationResult
                    {
                        Language = t.Language,
                        Value = t.Value
                    }).ToList()
                }).ToList()
            };
        }
    }
}
