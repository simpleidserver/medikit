// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Domains;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Common.Application.Persistence
{
    public class InMemoryTranslationQueryRepository : ITranslationQueryRepository
    {
        private readonly ConcurrentBag<Translation> _translations;

        public InMemoryTranslationQueryRepository(ConcurrentBag<Translation> translations)
        {
            _translations = translations;
        }

        public Task<ICollection<Translation>> GetTranslations(IEnumerable<string> codes, CancellationToken token)
        {
            ICollection<Translation> translations = _translations.Where(_ => codes.Contains(_.Code)).ToList();
            return Task.FromResult(translations);
        }

        public Task<ICollection<Translation>> GetTranslations(IEnumerable<string> codes, string languageCode, CancellationToken token)
        {
            ICollection<Translation> translations = _translations.Where(_ => codes.Contains(_.Code) && _.LanguageCode == languageCode).ToList();
            return Task.FromResult(translations);
        }
    }
}
