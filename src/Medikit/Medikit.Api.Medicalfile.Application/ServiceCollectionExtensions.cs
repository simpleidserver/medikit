// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Bus;
using Medikit.Api.Medicalfile.Application.Domains;
using Medikit.Api.Medicalfile.Application.Medicalfile;
using Medikit.Api.Medicalfile.Application.Persistence;
using Medikit.Api.Medicalfile.Application.Persistence.InMemory;
using Medikit.Api.Medicalfile.Application.Prescription;
using System.Collections.Concurrent;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMedicalfileApplication(this IServiceCollection services)
        {
            services.AddTransient<IPrescriptionService, PrescriptionService>();
            services.AddTransient<IMedicalfileService, MedicalfileService>();
            var medicalfiles = new ConcurrentBag<MedicalfileAggregate>();
            services.AddSingleton<IMedicalfileCommandRepository>(new InMemoryMedicalfileCommandRepository(medicalfiles));
            services.AddSingleton<IMedicalfileQueryRepository>(new InMemoryMedicalfileQueryRepository(medicalfiles));
            services.AddTransient<IMessageBrokerConsumerGeneric<MedicalfileAddedEvent>, MedicalfileEventHandler>();
            return services;
        }
    }
}
