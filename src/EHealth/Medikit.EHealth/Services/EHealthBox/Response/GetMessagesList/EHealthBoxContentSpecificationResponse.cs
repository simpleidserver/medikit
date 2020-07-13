// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.EHealthBox.Response
{
    public class EHealthBoxContentSpecificationResponse
    {
        [XmlElement(ElementName = "ContentType")]
        public string ContentType { get; set; }
        [XmlElement(ElementName = "IsImportant")]
        public bool IsImportant { get; set; }
        [XmlElement(ElementName = "IsEncrypted")]
        public bool IsEncrypted { get; set; }
    }
}
