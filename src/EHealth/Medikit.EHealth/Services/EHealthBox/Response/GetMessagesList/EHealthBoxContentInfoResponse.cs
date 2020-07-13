// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.EHealthBox.Response
{
    public class EHealthBoxContentInfoResponse
    {
        [XmlElement(ElementName = "Title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "MimeType")]
        public string MimeType { get; set; }
        [XmlElement(ElementName = "HasFreeInformations")]
        public bool HasFreeInformations { get; set; }
        [XmlElement(ElementName = "HasAnnex")]
        public bool HasAnnex { get; set; }
    }
}
