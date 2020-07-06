// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.EHealth.Application.Domains;
using Medikit.Api.EHealth.Application.KMEHRReference;
using Medikit.Api.EHealth.Application.MedicinalProduct;
using Medikit.Api.EHealth.Application.Persistence;
using Medikit.Api.EHealth.Application.Persistence.InMemory;
using System.Collections.Concurrent;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEHealthApplication(this IServiceCollection services)
        {
            services.AddTransient<IMedicinalProductService, MedicinalProductService>();
            var referenceTables = new ConcurrentBag<KMEHRReferenceTable>();
            services.AddTransient<IKMEHRReferenceTableService, KMEHRReferenceTableService>();
            services.AddSingleton<IKMEHRReferenceTableQueryRepository>(new InMemoryKMEHRReferenceTableQueryRepository(referenceTables));
            return services;
        }
    }
}
