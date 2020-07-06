// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth;
using Medikit.EHealth.EHealthServices;
using Medikit.EHealth.ETK;
using Medikit.EHealth.ETK.Store;
using Medikit.EHealth.KeyStore;
using Medikit.EHealth.SAML;
using Medikit.EHealth.Services.CIVICS;
using Medikit.EHealth.Services.DICS;
using Medikit.EHealth.Services.KGSS;
using Medikit.EHealth.Services.Recipe;
using Medikit.EHealth.SOAP;
using System;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEHealth(this IServiceCollection services, Action<EHealthOptions> callback = null)
        {
            if (callback == null)
            {
                services.Configure<EHealthOptions>(c => { });
            }
            else
            {
                services.Configure<EHealthOptions>(callback);
            }

            services.AddHttpClient("soapClient")
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler
                    {
                        UseCookies = false
                    };
                });

            services.AddETK()
                .AddEHealthServices()
                .AddSAML()
                .AddServices()
                .AddSOAP()
                .AddKeyStore();
            return services;
        }

        private static IServiceCollection AddEHealthServices(this IServiceCollection services)
        {
            services.AddTransient<IEHealthAmpService, EHealthAmpService>();
            services.AddTransient<IEHealthPrescriptionService, EHealthPrescriptionService>();
            return services;
        }

        private static IServiceCollection AddETK(this IServiceCollection services)
        {
            services.AddTransient<IETKService, ETKService>();
            services.AddSingleton<IETKStore, InMemoryETKStore>();
            return services;
        }

        private static IServiceCollection AddSAML(this IServiceCollection services)
        {
            services.AddTransient<ISessionService, SessionService>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IDicsService, DicsService>();
            services.AddTransient<IETKService, ETKService>();
            services.AddTransient<IKGSSService, KGSSService>();
            services.AddTransient<IRecipeService, RecipeService>();
            services.AddTransient<ICIVICSService, CIVICSService>();
            return services;
        }

        private static IServiceCollection AddSOAP(this IServiceCollection services)
        {
            services.AddTransient<ISOAPClient, SOAPClient>();
            return services;
        }

        private static IServiceCollection AddKeyStore(this IServiceCollection services)
        {
            services.AddTransient<IKeyStoreManager, KeyStoreManager>();
            return services;
        }
    }
}
