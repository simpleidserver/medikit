// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Bus;
using Medikit.Api.Patient.Application;
using Medikit.Api.Patient.Application.Domains;
using Medikit.Api.Patient.Application.Domains.Events;
using Medikit.Api.Patient.Application.Persistence;
using Medikit.Api.Patient.Application.Persistence.InMemory;
using System.Collections.Concurrent;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPatientApplication(this IServiceCollection services)
        {
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IMessageBrokerConsumerGeneric<PatientAddedEvent>, PatientEventHandler>();
            var patients = new ConcurrentBag<PatientAggregate>();
            services.AddSingleton<IPatientCommandRepository>(new InMemoryPatientCommandRepository(patients));
            services.AddSingleton<IPatientQueryRepository>(new InMemoryPatientQueryRepository(patients));
            return services;
        }
    }
}
