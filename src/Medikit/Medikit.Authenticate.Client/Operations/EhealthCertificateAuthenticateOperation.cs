// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Authenticate.Client.Requests;
using Medikit.Authenticate.Client.Responses;
using Medikit.EHealth.SAML;
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.SOAP.DTOs;
using System;

namespace Medikit.Authenticate.Client.Operations
{
    public class EhealthCertificateAuthenticateOperation : BaseOperation
    {
        private readonly IServiceProvider _serviceProvider;

        public EhealthCertificateAuthenticateOperation(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override string Code => "EHEALTH_AUTH";
        public override string Response => "SAML_ASSERTION";

        public override BrowserExtensionResponse Handle(BrowserExtensionRequest request)
        {
            var sessionService = (ISessionService)_serviceProvider.GetService(typeof(ISessionService));
            SOAPEnvelope<SAMLResponseBody> soapEnv = null;
            try
            {
                soapEnv = sessionService.BuildFallbackSession().Result;
            }
            catch(Exception ex)
            {
                return BuildError(request, ex.ToString());
            }

            var assertion = soapEnv.Body.Response.Assertion;
            return BuildResponse(request, new SAMLAssertionTokenResponse(assertion.Serialize().ToString(), assertion.Conditions.NotBefore, assertion.Conditions.NotOnOrAfter));
        }
    }
}
