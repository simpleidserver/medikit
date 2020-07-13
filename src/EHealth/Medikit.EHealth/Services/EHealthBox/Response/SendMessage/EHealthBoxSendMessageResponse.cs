// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Common;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.EHealthBox.Response
{
    public class EHealthBoxSendMessageResponse
    {
        [XmlElement(ElementName = "Status", Namespace = "")]
        public EHealthStatus Status { get; set; }
    }
}
