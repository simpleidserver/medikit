using Medikit.Api.Patient.Application.Domains;
using Medikit.Api.Patient.Application.Persistence;
using Medikit.Api.Patient.Application.Persistence.InMemory;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Medikit.Api.Common.Application
{
    public static class MedikitServerBuilderExtensions
    {
        public static MedikitServerBuilder AddPatients(this MedikitServerBuilder builder, ConcurrentBag<PatientAggregate> patients)
        {
            builder.Services.AddSingleton<IPatientCommandRepository>(new InMemoryPatientCommandRepository(patients));
            builder.Services.AddSingleton<IPatientQueryRepository>(new InMemoryPatientQueryRepository(patients));
            return builder;
        }
    }
}
