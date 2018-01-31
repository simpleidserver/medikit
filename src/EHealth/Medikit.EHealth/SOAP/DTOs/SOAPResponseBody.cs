﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPResponseBody
    {
        [XmlElement("Fault")]
        public SOAPFault Fault { get; set; }
    }
}
