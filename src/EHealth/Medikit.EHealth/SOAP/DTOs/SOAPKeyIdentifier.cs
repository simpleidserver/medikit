// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPKeyIdentifier
    {
        [XmlAttribute(AttributeName = "ValueType")]
        public string ValueType { get; set; }
        [XmlText]
        public string Content { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.WSSE + "KeyIdentifier",
                new XAttribute("ValueType", ValueType),
                new XText(Content));
            return result;
        }
    }
}
