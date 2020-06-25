// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Security.Cryptography.Pkcs;

namespace Medikit.EHealth.KeyStore
{
    public interface IKeyStoreManager
    {
        MedikitCertificate GetOrgAuthCertificate();
        MedikitCertificate GetIdAuthCertificate();
        MedikitCertificate GetIdETKCertificate();
        MedikitCertificate GetOrgETKCertificate();
    }
}
