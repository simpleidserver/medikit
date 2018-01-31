// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace Medikit.Authenticate.Client.Responses
{
    public class ErrorResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
