// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
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
            services.AddMedikitApiApplication(opt => { })
                .AddPatients(new ConcurrentBag<PatientAggregate>
                {
                    new PatientAggregate
                    {
                        Id = Guid.NewGuid().ToString(),
                        Firstname = "thierry",
                        Lastname = "habart",
                        CreateDateTime = DateTime.UtcNow,
                        BirthDate = DateTime.UtcNow,
                        NationalIdentityNumber = "071089",
                        Version = 0,
                        PrescriberId = "admin"
                    }
                })
                .AddLanguages(new List<string>
                {
                    "nl", "en", "fr"
                });
            services.AddMvc(opts => opts.EnableEndpointRouting = false).AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
