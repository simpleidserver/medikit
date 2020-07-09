// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Response
{
    public class RidHistoryResult
    {
        [XmlElement(ElementName = "rid", Namespace = "")]
        public string Rid { get; set; }
        [XmlElement(ElementName = "prescriptionStatus", Namespace = "")]
        public string PrescriptionStatus { get; set; }
    }
}
