// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Metadata;
using Medikit.Api.Application.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly ITranslationQueryRepository _translationQueryRepository;
        private readonly ILanguageQueryRepository _languageQueryRepository;

        public TranslationService(ITranslationQueryRepository translationQueryRepository, ILanguageQueryRepository languageQueryRepository)
        {
            _translationQueryRepository = translationQueryRepository;
            _languageQueryRepository = languageQueryRepository;
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
