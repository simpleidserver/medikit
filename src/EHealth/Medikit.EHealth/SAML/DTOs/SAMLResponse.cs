// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLResponse
    {
        [XmlAttribute("InResponseTo")]
        public string InResponseTo { get; set; }
        [XmlAttribute("IssueInstant")]
        public DateTime IssueInstant { get; set; }
        [XmlElement("Status")]
        public SAMLStatus Status { get; set; }
        [XmlElement("Assertion", Namespace = Constants.Namespaces.SAML)]
        public SAMLAssertion Assertion { get; set; }
    }
}
