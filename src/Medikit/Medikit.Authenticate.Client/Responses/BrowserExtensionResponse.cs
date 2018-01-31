// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace Medikit.Authenticate.Client.Responses
{
    public class BrowserExtensionResponse
    {
        public BrowserExtensionResponse(string nonce, string type)
        {
            Nonce = nonce;
            Type = type;
        }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
