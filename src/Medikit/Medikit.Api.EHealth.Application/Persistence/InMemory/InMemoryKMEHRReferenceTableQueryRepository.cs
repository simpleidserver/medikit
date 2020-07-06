// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.EHealth.Application.Domains;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medikit.Api.EHealth.Application.Persistence.InMemory
{
    public class InMemoryKMEHRReferenceTableQueryRepository : IKMEHRReferenceTableQueryRepository
    {
        private readonly ConcurrentBag<KMEHRReferenceTable> _referenceTables;

        public InMemoryKMEHRReferenceTableQueryRepository(ConcurrentBag<KMEHRReferenceTable> referenceTables)
        {
            _referenceTables = referenceTables;
        }

        public Task<KMEHRReferenceTable> GetByCode(string code, string language = null)
        {
            var result = _referenceTables.FirstOrDefault(r => r.Code == code);
            if (result == null)
            {
                return Task.FromResult((KMEHRReferenceTable)null);
            }

            result = (KMEHRReferenceTable)result.Clone();
            if (!string.IsNullOrWhiteSpace(language))
            {
                foreach (var record in result.Content)
                {
                    record.Translations = record.Translations.Where(t => t.Language == language).ToList();
                }
            }

            return Task.FromResult(result);
        }

        public Task<IEnumerable<string>> GetAllCodes()
        {
            return Task.FromResult(_referenceTables.Select(r => r.Code));
        }
    }
}
