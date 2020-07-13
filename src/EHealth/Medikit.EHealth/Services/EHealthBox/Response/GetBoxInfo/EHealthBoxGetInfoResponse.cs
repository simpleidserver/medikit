// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Common;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.EHealthBox.Response
{
    public class EHealthBoxGetInfoResponse
    {
        [XmlElement(ElementName = "Status", Namespace = "")]
        public EHealthStatus Status { get; set; }
        [XmlElement(ElementName = "BoxId", Namespace = "")]
        public EHealthBoxIdType BoxId { get; set; }
        [XmlElement(ElementName = "NbrMessagesInStandBy", Namespace = "")]
        public int NbrMessagesInStandBy { get; set; }
        [XmlElement(ElementName = "CurrentSize", Namespace = "")]
        public int CurrentSize { get; set; }
        [XmlElement(ElementName = "MaxSize", Namespace = "")]
        public int MaxSize { get; set; }
    }
}
