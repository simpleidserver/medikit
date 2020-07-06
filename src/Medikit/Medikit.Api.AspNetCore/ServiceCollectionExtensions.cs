// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application;
using Medikit.EHealth;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static MedikitServerBuilder AddMedikitApi(this IServiceCollection services, Action<MedikitServerOptions> medikitCallack = null, Action<EHealthOptions> eheathCallback = null)
        {
            if (medikitCallack != null)
            {
                services.Configure(medikitCallack);
            }

            services.AddCommon()
                .AddEHealthApplication()
                .AddMedicalfileApplication()
                .AddPatientApplication()
                .AddQRFileApplication()
                .AddEHealth(eheathCallback);
            return new MedikitServerBuilder(services);
        }
    }
}
