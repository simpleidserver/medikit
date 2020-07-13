// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Common
{
    public class EHealthStatus
    {
        [XmlElement("Code", Namespace = "")]
        public int Code { get; set; }
        [XmlElement("Message", Namespace = "")]
        public EHealthMessage Message { get; set; }
    }
}