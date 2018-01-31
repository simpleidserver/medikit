// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.Services.KGSS.Response;
using Medikit.EHealth.Services.KGSS.Response.GetKey;
using System.Threading.Tasks;

namespace Medikit.EHealth.Services.KGSS
{
    public interface IKGSSService
    {
        Task<KGSSGetKeyResponseContent> GetKeyFromKGSS(string keyId, SAMLAssertion assertion);
        Task<KGSSGetNewKeyResponseContent> GetOrgKGSS();
    }
}
