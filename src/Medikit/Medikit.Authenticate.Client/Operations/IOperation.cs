// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Authenticate.Client.Requests;
using Medikit.Authenticate.Client.Responses;

namespace Medikit.Authenticate.Client.Operations
{
    public interface IOperation
    {
        string Code { get; }
        string Response { get; }
        BrowserExtensionResponse Handle(BrowserExtensionRequest request);
    }
}
