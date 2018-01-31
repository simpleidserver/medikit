// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLNameIdentifier
    {
        [XmlAttribute(AttributeName = "Format")]
        public string Format { get; set; }
        [XmlAttribute(AttributeName = "NameQualifier")]
        public string NameQualifier { get; set; }
        [XmlText]
        public string Content { get; set; }

        public XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.SAML + "NameIdentifier",
                new XAttribute("Format", Format),
                new XAttribute("NameQualifier", NameQualifier),
                new XText(Content));
        }
    }
}
