// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace Medikit.Authenticate.Client.Responses
{
    public class GetIdentityCertificateResponse
    {
        public GetIdentityCertificateResponse(string certificate, string password)
        {
            Certificate = certificate;
            Password = password;
        }

        [JsonProperty("certificate")]
        public string Certificate { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
