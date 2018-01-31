// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Serialization;

namespace Medikit.EHealth.ETK.Response
{
    public class ETKMessage
    {
        [XmlAttribute("Lang")]
        public string Lang { get; set; }
        [XmlText]
        public string Content { get; set; }
    }
}
