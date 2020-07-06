// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Domains;
using Medikit.Api.Common.Application.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Common.Application.Metadata
{
    public class InMemoryTranslationService : ITranslationService
    {
        private readonly ILanguageQueryRepository _languageQueryRepository;
        private readonly ITranslationQueryRepository _translationQueryRepository;

        public InMemoryTranslationService(ILanguageQueryRepository languageQueryRepository, ITranslationQueryRepository translationQueryRepository)
        {
            _languageQueryRepository = languageQueryRepository;
            _translationQueryRepository = translationQueryRepository;
        }

        public Task<ICollection<Language>> GetLanguages(CancellationToken token)
        {
            return _languageQueryRepository.GetLanguages(token);
        }

        public Task<ICollection<Translation>> GetTranslations(IEnumerable<string> codes, CancellationToken token)
        {
            return _translationQueryRepository.GetTranslations(codes, token);
        }

        public Task<ICollection<Translation>> GetTranslations(IEnumerable<string> codes, string languageCode, CancellationToken token)
        {
            return _translationQueryRepository.GetTranslations(codes, languageCode, token);
        }
    }
}
