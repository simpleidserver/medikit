// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.CIVICS;
using Medikit.EHealth.Services.CIVICS.Request;
using System.Threading.Tasks;

namespace Medikit.EHealth.Console
{
    public partial class Program
    {
        public static async Task FindCNKCivics()
        {
            var civicsClient = (ICIVICSService)_serviceProvider.GetService(typeof(ICIVICSService));
            await civicsClient.FindCNK(new CIVICSFindCNKRequest
            {
                Language = "fr",
                ProductName = "clamoxyl",
                // ChapterName = "IV",
                // ParagraphName = "30200"
            });
        }
    }
}
