// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace Medikit.EHealth.EHealthServices.Results
{
    public class AmppResult
    {
        public AmppResult()
        {
            LeafletUrlLst = new List<EHealthTranslationResult>();
            SpcUrlLst = new List<EHealthTranslationResult>();
            CrmUrlLst = new List<EHealthTranslationResult>();
            PrescriptionNames = new List<EHealthTranslationResult>();
            DeliveryMethods = new List<DmppResult>();
        }

        public ICollection<EHealthTranslationResult> LeafletUrlLst { get; set; }
        public ICollection<EHealthTranslationResult> SpcUrlLst { get; set; }
        public ICollection<EHealthTranslationResult> CrmUrlLst { get; set; }
        public ICollection<EHealthTranslationResult> PrescriptionNames { get; set; }
        public ICollection<DmppResult> DeliveryMethods { get; set; }
    }
}
