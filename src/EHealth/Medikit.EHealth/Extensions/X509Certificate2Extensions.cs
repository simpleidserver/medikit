// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Medikit.EHealth.Extensions
{
    public static class X509Certificate2Extensions
    {
        public static string SerializeToString(this X509Certificate2 cert)
        {
            return Convert.ToBase64String(cert.GetRawCertData());
        }

        public static string ExtractSSIN(this X509Certificate2 cert)
        {
            var regex = new Regex("SSIN=[0-9]{11}");
            var matches = regex.Matches(cert.Subject);
            if(matches.Count == 0)
            {
                return null;
            }

            return matches[0].Value.Split('=').Last();
        }

        public static string ExtractCBE(this X509Certificate2 cert)
        {
            var regex = new Regex("CBE=[0-9]{10}");
            var matches = regex.Matches(cert.Subject);
            if (matches.Count == 0)
            {
                return null;
            }

            return matches[0].Value.Split('=').Last();
        }
    }
}
