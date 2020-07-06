// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application;
using Medikit.Api.Common.Application.Bus;
using Medikit.Api.Common.Application.Bus.InMemory;
using Medikit.Api.Common.Application.Caching;
using Medikit.Api.Common.Application.Domains;
using Medikit.Api.Common.Application.EvtStore;
using Medikit.Api.Common.Application.EvtStore.InMemory;
using Medikit.Api.Common.Application.Metadata;
using Medikit.Api.Common.Application.Persistence;
using NEventStore;
using System.Collections.Concurrent;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            var wireup = Wireup.Init().UsingInMemoryPersistence().Build();
            services.AddSingleton<IStoreEvents>(wireup);
            services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
            services.AddSingleton<ICommitAggregateHelper, CommitAggregateHelper>();
            services.AddSingleton<IAggregateSnapshotStore, InMemoryAggregateSnapshotStore>();
            services.AddTransient<IMetadataResultBuilder, MetadataResultBuilder>();
            services.AddTransient<ITranslationService, InMemoryTranslationService>();
            var languages = new ConcurrentBag<Language>();
            var translations = new ConcurrentBag<Translation>();
            services.AddSingleton<ILanguageQueryRepository>(new InMemoryLanguageQueryRepository(languages));
            services.AddSingleton<ITranslationQueryRepository>(new InMemoryTranslationQueryRepository(translations));
            services.AddSingleton<ICacheStore, InMemoryCacheStore>();
            return services;
        }
    }
}
