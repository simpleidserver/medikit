// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Serialization;

namespace Medikit.EHealth.ETK.Response
{
    public class ETKError
    {
        [XmlElement("Code")]
        public string Code { get; set; }
        [XmlElement("Message")]
        public string Message { get; set; }
    }
}
