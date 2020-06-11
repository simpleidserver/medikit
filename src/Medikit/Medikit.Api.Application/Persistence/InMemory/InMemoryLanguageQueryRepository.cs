// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Persistence.InMemory
{
    public class InMemoryLanguageQueryRepository : ILanguageQueryRepository
    {
        private readonly ConcurrentBag<Language> _languages;

        public InMemoryLanguageQueryRepository(ConcurrentBag<Language> languages)
        {
            _languages = languages;
        }

        public Task<ICollection<Language>> GetLanguages(CancellationToken token)
        {
            ICollection<Language> result = _languages.ToList();
            return Task.FromResult(result);
        }
    }
}
