// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Medikit.Authenticate.Client.Responses
{
    public class GetMedicalProfessionsResponse
    {
        [JsonProperty("current_profession")]
        public string CurrentProfession { get; set; }
        [JsonProperty("professions")]
        public ICollection<MedicalProfessionResponse> Professions { get; set; }
    }
}
