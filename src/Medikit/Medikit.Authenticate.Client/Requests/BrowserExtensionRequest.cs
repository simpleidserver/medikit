// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Medikit.Authenticate.Client.Requests
{
    public class BrowserExtensionRequest
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        [JsonProperty("content")]
        public JObject Content { get; set; }
    }
}