// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Medikit.Api.Common.Application.Domains;
using Medikit.Api.Common.Application.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Medikit.Api.Common.Application
{
    public class MedikitServerBuilder
    {
        private IServiceCollection _services;

        public MedikitServerBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public IServiceCollection Services => _services;

        public MedikitServerBuilder AddLanguages(ICollection<string> lst)
        {
            var languages = new ConcurrentBag<Language>();
            foreach (var record in lst)
            {
                languages.Add(new Language
                {
                    Code = record,
                    CreateDateTime = DateTime.UtcNow,
                    UpdateDateTime = DateTime.UtcNow
                });
            }

            _services.AddSingleton<ILanguageQueryRepository>(new InMemoryLanguageQueryRepository(languages));
            return this;
        }
    }
}
