// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.SOAP.DTOs;
using System.Threading.Tasks;

namespace Medikit.EHealth.SAML
{
    public interface ISessionService
    {
        SOAPEnvelope<SAMLResponseBody> GetSession();
        Task<SOAPEnvelope<SAMLResponseBody>> BuildFallbackSession();
        Task<SOAPEnvelope<SAMLResponseBody>> BuildEIDSession(string pin);
    }
}
