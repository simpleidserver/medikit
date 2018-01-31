// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;
using System;

namespace Medikit.Authenticate.Client.Responses
{
    public class SAMLAssertionTokenResponse
    {
        public SAMLAssertionTokenResponse(string assertionToken, DateTime notBefore, DateTime notOnOrAfter)
        {
            AssertionToken = assertionToken;
            NotBefore = notBefore;
            NotOnOrAfter = notOnOrAfter;
        }

        [JsonProperty("assertion_token")]
        public string AssertionToken { get; set; }
        [JsonProperty("not_before")]
        public DateTime NotBefore { get; set; }
        [JsonProperty("not_onorafter")]
        public DateTime NotOnOrAfter { get; set; }
    }
}
