// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Authenticate.Client.Operations;
using Medikit.EHealth.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace Medikit.Authenticate.Client
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
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
            RegisterService(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");
            app.UseMvc();
        }

        private IServiceCollection RegisterService(IServiceCollection services)
        {
            var certificatesPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Certificates");
            services.AddTransient<IOperation, PingOperation>();
            services.AddTransient<IOperation, ChooseIdentityCertificateOperation>();
            services.AddTransient<IOperation, ChooseMedicalProfessionOperation>();
            services.AddTransient<IOperation, EhealthCertificateAuthenticateOperation>();
            services.AddTransient<IOperation, EidAuthenticateOperation>();
            services.AddTransient<IOperation, GetIdentityCertificatesOperation>();
            services.AddTransient<IOperation, GetMedicalProfessionsOperation>();
            services.AddSingleton(typeof(IConfiguration), _configuration);
            services.AddEHealth(_ =>
            {
                _.IdentityCertificateStore = Path.Combine(certificatesPath, _configuration[Constants.ConfigurationNames.IdentityCertificateStore]);
                _.IdentityCertificateStorePassword = _configuration[Constants.ConfigurationNames.IdentityCertificateStorePassword];
                _.OrgCertificateStore = Path.Combine(certificatesPath, _configuration[Constants.ConfigurationNames.OrgCertificateStoreFile]);
                _.OrgCertificateStorePassword = _configuration[Constants.ConfigurationNames.OrgCertificateStorePassword];
                _.IdentityProfession = Enumeration.Get<MedicalProfessions>(_configuration[Constants.ConfigurationNames.Profession]);
            });
            return services;
        }
    }
}
