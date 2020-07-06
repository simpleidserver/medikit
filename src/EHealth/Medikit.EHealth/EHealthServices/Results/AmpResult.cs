// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace Medikit.EHealth.EHealthServices.Results
{
    public class AmpResult
    {
        public AmpResult()
        {
            Names = new List<EHealthTranslationResult>();
            AmppLst = new List<AmppResult>();
        }

        public string Code { get; set; }
        public string OfficialName { get; set; }
        public List<EHealthTranslationResult> Names { get; set; }
        public List<AmppResult> AmppLst { get; set; }
    }
}
