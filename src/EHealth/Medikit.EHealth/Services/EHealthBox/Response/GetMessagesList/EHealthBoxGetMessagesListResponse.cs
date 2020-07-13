// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Common;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.EHealthBox.Response
{
    public class EHealthBoxGetMessagesListResponse
    {
        [XmlElement(ElementName = "Status", Namespace = "")]
        public EHealthStatus Status { get; set; }
        [XmlElement(ElementName = "Source", Namespace = "")]
        public string Source { get; set; }
        [XmlElement(ElementName = "Message", Namespace = "")]
        public List<EHealthBoxMessageResponse> MessageLst { get; set; }
    }
}
