// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Common;
using System.Collections.Generic;

namespace Medikit.Api.Application.Services.Results
{
    public class AmpResult
    {
        public AmpResult()
        {
            Names = new List<TranslationResult>();
            AmppLst = new List<AmppResult>();
        }

        public string Code { get; set; }
        public string OfficialName { get; set; }
        public List<TranslationResult> Names { get; set; }
        public List<AmppResult> AmppLst { get; set; }
    }
}
