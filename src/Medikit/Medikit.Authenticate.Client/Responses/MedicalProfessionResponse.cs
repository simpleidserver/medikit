// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace Medikit.Authenticate.Client.Responses
{
    public class MedicalProfessionResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("namespace")]
        public string Namespace { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
    }
}
