// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Medikit.EHealth.Extensions
{
    public static class CollectionExtensions
    {
        public static X509Certificate2Collection ToCertificateCollection(this ICollection<X509Certificate2> certificates)
        {
            var col = new X509Certificate2Collection();
            foreach(var certificate in certificates)
            {
                col.Add(certificate);
            }

            return col;
        }
    }
}
