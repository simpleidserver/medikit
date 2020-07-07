using Medikit.Api.Medicalfile.Application.Domains;
using Medikit.Api.Medicalfile.Application.Persistence;
using Medikit.Api.Medicalfile.Application.Persistence.InMemory;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Medikit.Api.Common.Application
{
    public static class MedikitServerBuilderExtensions
    {
        public static MedikitServerBuilder AddMedicalfiles(this MedikitServerBuilder builder, ConcurrentBag<MedicalfileAggregate> medicalfiles)
        {
            builder.Services.AddSingleton<IMedicalfileCommandRepository>(new InMemoryMedicalfileCommandRepository(medicalfiles));
            builder.Services.AddSingleton<IMedicalfileQueryRepository>(new InMemoryMedicalfileQueryRepository(medicalfiles));
            return builder;
        }
    }
}
