// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Security.Cryptography.X509Certificates;

namespace Medikit.EHealth.KeyStore
{
    public interface IKeyStoreManager
    {
        X509Certificate2 GetOrgAuthCertificate();
        X509Certificate2 GetIdAuthCertificate();
        X509Certificate2 GetIdETKCertificate();
        X509Certificate2 GetOrgETKCertificate();
    }
}
