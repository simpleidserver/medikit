﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLStatus
    {
        [XmlElement("StatusCode")]
        public SAMLStatusCode StatusCode { get; set; }
    }
}
