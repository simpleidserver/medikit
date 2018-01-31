// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPBinarySecurityToken
    {
        [XmlAttribute(AttributeName = "EncodingType")]
        public string EncodingType { get; set; }
        [XmlAttribute(AttributeName = "ValueType")]
        public string ValueType { get; set; }
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
        [XmlText]
        public string Content { get; set; }

        public XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.WSSE + "BinarySecurityToken",
                new XAttribute("EncodingType", EncodingType),
                new XAttribute("ValueType", ValueType),
                new XAttribute(Constants.XMLNamespaces.WSU + "Id", Id),
                new XText(Content));
        }
    }
}
