// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SAML;
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.SOAP.DTOs;
using System.Threading.Tasks;

namespace Medikit.EHealth.Console
{
    public partial class Program
    {

        private static async Task<SOAPEnvelope<SAMLResponseBody>> BuildSTSIdentityRequest()
        {
            var sessionService = (ISessionService)_serviceProvider.GetService(typeof(ISessionService));
            var result = await sessionService.BuildFallbackSession();
            return result;
        }

        private static async Task<SOAPEnvelope<SAMLResponseBody>> BuildSTSIdentityRequestEID()
        {
            var sessionService = (ISessionService)_serviceProvider.GetService(typeof(ISessionService));
            var result = await sessionService.BuildEIDSession(string.Empty);
            return result;
        }
    }
}
