// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Authenticate.Client.Requests;
using Medikit.Authenticate.Client.Responses;
using System;

namespace Medikit.Authenticate.Client.Operations
{
    public class PingOperation : BaseOperation
    {
        public override string Code => "PING";
        public override string Response => "PONG";

        public override BrowserExtensionResponse Handle(BrowserExtensionRequest request)
        {
            return BuildResponse(request, new PongResponse(DateTime.UtcNow));
        }
    }
}
