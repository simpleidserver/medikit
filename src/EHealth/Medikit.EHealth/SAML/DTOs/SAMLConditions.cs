// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Extensions;
using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLConditions
    {
        [XmlAttribute(AttributeName = "NotBefore")]
        public DateTime NotBefore { get; set; }
        [XmlAttribute(AttributeName = "NotOnOrAfter")]
        public DateTime NotOnOrAfter { get; set; }

        public XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.SAML + "Conditions",
                new XAttribute("NotBefore", NotBefore.ToUTCString()),
                new XAttribute("NotOnOrAfter", NotOnOrAfter.ToUTCString()));
        }
    }
}
