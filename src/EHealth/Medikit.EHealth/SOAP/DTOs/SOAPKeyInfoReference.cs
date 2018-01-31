// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPKeyInfoReference
    {
        [XmlAttribute(AttributeName = "URI")]
        public string Uri { get; set; }
        [XmlAttribute(AttributeName = "ValueType")]
        public string ValueType { get; set; }

        public XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.WSSE + "Reference",
                new XAttribute("URI", Uri),
                new XAttribute("ValueType", ValueType));
        }
    }
}
