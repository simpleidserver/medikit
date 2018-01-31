// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Medikit.EHealth.Services.KGSS;
using Medikit.EHealth.Services.KGSS.Response;
using System.Threading.Tasks;

namespace Medikit.EHealth.Console
{
    public partial class Program
    {
        private static async Task<KGSSGetNewKeyResponseContent> GetKGSS()
        {
            var kgssService = (IKGSSService)_serviceProvider.GetService(typeof(IKGSSService));
            var result = await kgssService.GetOrgKGSS();
            return result;
        }
    }
}
