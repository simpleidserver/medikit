// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLAttribute
    {
        [XmlAttribute(AttributeName = "AttributeName")]
        public string AttributeName { get; set; }
        [XmlAttribute(AttributeName = "AttributeNamespace")]
        public string AttributeNamespace { get; set; }
        [XmlElement(ElementName = "AttributeValue", Namespace = "urn:oasis:names:tc:SAML:1.0:assertion")]
        public string AttributeValue { get; set; }

        public XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.SAML + "Attribute",
                new XAttribute("AttributeName", AttributeName),
                new XAttribute("AttributeNamespace", AttributeNamespace),
                new XElement(Constants.XMLNamespaces.SAML + "AttributeValue", AttributeValue));
        }
    }
}
