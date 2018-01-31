// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLAttributeDesignator
    {
        [XmlAttribute(AttributeName = "AttributeName")]
        public string AttributeName { get; set; }
        [XmlAttribute(AttributeName = "AttributeNamespace")]
        public string AttributeNamespace { get; set; }

        public XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.SAML + "AttributeDesignator",
                new XAttribute("AttributeName", AttributeName),
                new XAttribute("AttributeNamespace", AttributeNamespace));
        }
    }
}
