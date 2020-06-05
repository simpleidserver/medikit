// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application;
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Domains.Events;
using Medikit.Api.Application.Infrastructure;
using Medikit.Api.Application.Infrastructure.Bus;
using Medikit.Api.Application.Infrastructure.Bus.InMemory;
using Medikit.Api.Application.Infrastructure.EvtStore;
using Medikit.Api.Application.Infrastructure.EvtStore.InMemory;
using Medikit.Api.Application.MedicinalProduct;
using Medikit.Api.Application.MedicinalProduct.Queries.Handlers;
using Medikit.Api.Application.Patient;
using Medikit.Api.Application.Patient.Commands.Handlers;
using Medikit.Api.Application.Patient.Queries.Handlers;
using Medikit.Api.Application.Persistence;
using Medikit.Api.Application.Persistence.InMemory;
using Medikit.Api.Application.Prescriptions;
using Medikit.Api.Application.Prescriptions.Commands.Handlers;
using Medikit.Api.Application.Prescriptions.Queries.Handlers;
using Medikit.Api.Application.Reference;
using Medikit.Api.Application.Reference.Queries.Handlers;
using Medikit.Api.Application.Services;
using Medikit.Api.Application.Services.EHealth;
using Medikit.Api.Application.Services.InMemory;
using Medikit.EHealth;
using NEventStore;
using System;
using System.Collections.Concurrent;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static MedikitServerBuilder AddMedikitApiApplication(this IServiceCollection services, 
            Action<MedikitServerOptions> medikitCallack = null,
            Action<EHealthOptions> eheathCallback = null)
        {
            if (medikitCallack != null)
            {
                services.Configure(medikitCallack);
            }

            services.AddCommon()
                .AddPatient()
                .AddNomenclature()
                .AddMedicinalPrescription()
                .AddReferenceTable()
                // .AddInMemoryServices()
                .AddEHealthServices(eheathCallback)
                .AddMessageHandlers()
                .AddPersistence();
            return new MedikitServerBuilder(services);
        }

        private static IServiceCollection AddCommon(this IServiceCollection services)
        {
            var wireup = Wireup.Init().UsingInMemoryPersistence().Build();
            services.AddSingleton<IStoreEvents>(wireup);
            services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
            services.AddSingleton<ICommitAggregateHelper, CommitAggregateHelper>();
            services.AddSingleton<IAggregateSnapshotStore, InMemoryAggregateSnapshotStore>();
            return services;
        }

        private static IServiceCollection AddPatient(this IServiceCollection services)
        {
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IGetPatientByNissQueryHandler, GetPatientByNissQueryHandler>();
            services.AddTransient<IAddPatientCommandHandler, AddPatientCommandHandler>();
            services.AddTransient<ISearchPatientsQueryHandler, SearchPatientsQueryHandler>();
            return services;
        }

        private static IServiceCollection AddNomenclature(this IServiceCollection services)
        {
            services.AddTransient<IMedicinalProductService, MedicinalProductService>();
            services.AddTransient<ISearchMedicinalProductHandler, SearchMedicinalProductHandler>();
            return services;
        }

        private static IServiceCollection AddMedicinalPrescription(this IServiceCollection services)
        {
            services.AddTransient<IPharmaceuticalPrescriptionService, PharmaceuticalPrescriptionService>();
            services.AddTransient<IGetPharmaceuticalPrescriptionQueryHandler, GetPharmaceuticalPrescriptionQueryHandler>();
            services.AddTransient<IGetOpenedPharmaceuticalPrescriptionQueryHandler, GetOpenedPharmaceuticalPrescriptionQueryHandler>();
            services.AddTransient<IAddPharmaceuticalPrescriptionCommandHandler, AddPharmaceuticalPrescriptionCommandHandler>();
            services.AddTransient<IPharmaceuticalPrescriptionService, PharmaceuticalPrescriptionService>();
            return services;
        }

        private static IServiceCollection AddReferenceTable(this IServiceCollection services)
        {
            services.AddTransient<IReferenceTableService, ReferenceTableService>();
            services.AddTransient<IGetReferenceByCodeQueryHandler, GetReferenceByCodeQueryHandler>();
            services.AddTransient<IGetAllReferenceCodesQueryHandler, GetAllReferenceCodesQueryHandler>();
            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            var referenceTables = new ConcurrentBag<ReferenceTable>();
            var patients = new ConcurrentBag<PatientAggregate>();
            services.AddSingleton<IReferenceTableQueryRepository>(new InMemoryReferenceTableQueryRepository(referenceTables));
            services.AddSingleton<IPatientCommandRepository>(new InMemoryPatientCommandRepository(patients));
            services.AddSingleton<IPatientQueryRepository>(new InMemoryPatientQueryRepository(patients));
            return services;
        }

        private static IServiceCollection AddInMemoryServices(this IServiceCollection services)
        {
            services.AddSingleton<IPrescriptionService, InMemoryPrescriptionService>();
            services.AddSingleton<IAmpService, InMemoryAmpService>();
            return services;
        }

        private static IServiceCollection AddEHealthServices(this IServiceCollection services, Action<EHealthOptions> callback = null)
        {
            services.AddTransient<IAmpService, EHealthAmpService>();
            services.AddTransient<IPrescriptionService, EHealthPrescriptionService>();
            services.AddEHealth(callback);
            return services;
        }

        private static IServiceCollection AddMessageHandlers(this IServiceCollection services)
        {
            services.AddTransient<IMessageBrokerConsumerGeneric<PatientAddedEvent>, PatientEventHandler>();
            return services;
        }
    }
}
