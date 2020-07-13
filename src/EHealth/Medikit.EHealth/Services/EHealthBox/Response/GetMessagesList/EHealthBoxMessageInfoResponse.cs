// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.EHealthBox.Response
{
    public class EHealthBoxMessageInfoResponse
    {
        [XmlElement(ElementName = "PublicationDate")]
        public DateTime PublicationDate { get; set; }
        [XmlElement(ElementName = "ExpirationDate")]
        public DateTime ExpirationDate { get; set; }
        [XmlElement(ElementName = "Size")]
        public int Size { get; set; }
    }
}
