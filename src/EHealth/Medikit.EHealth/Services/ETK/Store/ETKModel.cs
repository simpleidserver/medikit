// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Security.Cryptography.X509Certificates;

namespace Medikit.EHealth.Services.ETK.Store
{
    public class ETKModel
    {
        public ETKModel(X509Certificate2 certificate, string etk)
        {
            Certificate = certificate;
            ETK = etk;
        }

        public X509Certificate2 Certificate { get; set; }
        public string ETK { get; set; }
    }
}
