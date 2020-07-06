// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.QRFile.Application;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQRFileApplication(this IServiceCollection services)
        {
            services.AddTransient<IQRFileService, QRFileService>();
            return services;
        }
    }
}
