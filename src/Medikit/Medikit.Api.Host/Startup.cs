// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Medikit.Api.Host
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment  env) 
        {
            _configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opts => opts.EnableEndpointRouting = false).AddNewtonsoftJson();
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));
            var certificatesPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Certificates");
            services.AddMedikitApiApplication(_ =>
            {
                _.RootPath = _env.WebRootPath;
            }, eheathCallback: o =>
            {
                o.OrgCertificateStore = Path.Combine(certificatesPath, "CBE=0543979265 20200417-143522.acc-p12");
                o.OrgCertificateStorePassword = "AJH9ka/fh%.?75WF";
            })
                .ImportTableReference(Path.Combine(Directory.GetCurrentDirectory(), "ReferenceTables"))
                .AddPatients(new ConcurrentBag<PatientAggregate>
                {
                    new PatientAggregate
                    {
                        Id = Guid.NewGuid().ToString(),
                        Firstname = "Fred",
                        Lastname = "Flintstone",
                        CreateDateTime = DateTime.UtcNow,
                        BirthDate = DateTime.UtcNow,
                        NationalIdentityNumber = "76020727360",
                        Version = 0,
                        PrescriberId = "admin",
                        UpdateDateTime = DateTime.UtcNow
                    }
                })
                .AddLanguages(new List<string>
                {
                    "nl", "en", "fr"
                });
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (_configuration.GetChildren().Any(i => i.Key == "pathBase"))
            {
                app.UsePathBase(_configuration["pathBase"]);
            }

            app.UseStaticFiles();
            app.UseForwardedHeaders();
            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseMvc();
        }
    }
}