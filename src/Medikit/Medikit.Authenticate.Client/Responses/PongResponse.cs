// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;
using System;

namespace Medikit.Authenticate.Client.Responses
{
    public class PongResponse
    {
        public PongResponse(DateTime issueDateTime)
        {
            IssueDateTime = issueDateTime;
        }

        [JsonProperty("issue_datetime")]
        public DateTime IssueDateTime { get; set; }
    }
}
