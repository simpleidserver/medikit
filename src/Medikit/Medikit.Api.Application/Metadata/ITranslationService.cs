// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Metadata
{
    public interface ITranslationService
    {
        Task<ICollection<Translation>> GetTranslations(IEnumerable<string> codes, CancellationToken token);
        Task<ICollection<Translation>> GetTranslations(IEnumerable<string> codes, string languageCode, CancellationToken token);
        Task<ICollection<Language>> GetLanguages(CancellationToken token);
    }
}
