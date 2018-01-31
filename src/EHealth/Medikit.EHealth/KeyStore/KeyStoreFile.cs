// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Diagnostics;

namespace Medikit.EHealth.KeyStore
{
    public class KeyStoreFile
    {
        public KeyStoreFile()
        {
            Certificates = new List<KeyStoreCertificate>();
        }

        public int VersionNumber { get; set; }
        public ICollection<KeyStoreCertificate> Certificates { get; set; }
    }
}
