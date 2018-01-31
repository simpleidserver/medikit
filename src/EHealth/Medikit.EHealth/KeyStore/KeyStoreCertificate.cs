// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Medikit.EHealth.KeyStore
{
    [DebuggerDisplay("{Certificate}")]
    public class KeyStoreCertificate
    {
        public string Name { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Type { get; set; }
        public X509Certificate2 Certificate { get; set; }
    }
}
