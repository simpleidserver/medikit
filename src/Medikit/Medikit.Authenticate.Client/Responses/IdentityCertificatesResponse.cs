// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Medikit.Authenticate.Client.Responses
{
    public class IdentityCertificatesResponse
    {
        public IdentityCertificatesResponse(string currentCertificate, ICollection<string> certificates)
        {
            CurrentCertificate = currentCertificate;
            Certificates = certificates;
        }

        [JsonProperty("current_certificate")]
        public string CurrentCertificate { get; set; }
        [JsonProperty("certificates")]
        public ICollection<string> Certificates { get; set; }
    }
}
