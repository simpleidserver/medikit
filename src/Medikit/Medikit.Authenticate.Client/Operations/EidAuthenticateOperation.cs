// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Medikit.Authenticate.Client.Requests;
using Medikit.Authenticate.Client.Responses;

namespace Medikit.Authenticate.Client.Operations
{
    public class EidAuthenticateOperation : BaseOperation
    {
        public override string Code => "EID_AUTH";
        public override string Response => "SAML_ASSERTION";

        public override BrowserExtensionResponse Handle(BrowserExtensionRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
