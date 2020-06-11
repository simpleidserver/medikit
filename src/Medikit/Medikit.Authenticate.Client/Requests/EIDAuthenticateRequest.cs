// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace Medikit.Authenticate.Client.Requests
{
    public class EIDAuthenticateRequest
    {
        [JsonProperty("pin")]
        public string Pin { get; set; }
    }
}
