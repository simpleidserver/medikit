// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application;
using Medikit.Api.EHealth.Application.MedicinalProduct.Queries.Handlers;
using Medikit.Api.Medicalfile.Application.Prescription.Commands.Handlers;
using Medikit.Api.Patient.Application.Domains;
using Medikit.Api.Patient.Application.Queries.Handlers;
using Medikit.Api.QRFile.Application.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Medikit.Api.Acceptance.Tests
{
    public class FakeStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMedikitApi(opt => { })
                .AddPatients(new ConcurrentBag<PatientAggregate>
                {
                    new PatientAggregate
                    {
                        Id = "1",
                        Firstname = "thierry",
                        Lastname = "habart",
                        CreateDateTime = DateTime.UtcNow,
                        BirthDate = DateTime.UtcNow,
                        NationalIdentityNumber = "071089",
                        Version = 0
                    }
                })
                .AddLanguages(new List<string>
                {
                    "nl", "en", "fr"
                });
            services.AddMediatR(
                typeof(SearchMedicinalPackageHandler), 
                typeof(AddPharmaceuticalPrescriptionCommandHandler),
                typeof(GetPatientByIdQueryHandler),
                typeof(GetQRFileQuery));
            services.AddMvc(opts => opts.EnableEndpointRouting = false).AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
