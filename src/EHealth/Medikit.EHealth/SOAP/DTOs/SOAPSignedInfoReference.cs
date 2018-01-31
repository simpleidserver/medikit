// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPSignedInfoReference
    {
        [XmlAttribute(AttributeName = "URI")]
        public string Uri { get; set; }
        [XmlElement(ElementName = "Transforms", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SOAPTransforms Transforms { get; set; }
        [XmlElement(ElementName = "DigestMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SOAPDigestMethod DigestMethod { get; set; }
        [XmlElement(ElementName = "DigestValue", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public string DigestValue { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.DS + "Reference",
                new XAttribute("URI", Uri),
                Transforms.Serialize(),
                DigestMethod.Serialize(),
                new XElement(Constants.XMLNamespaces.DS + "DigestValue", DigestValue));
            return result;
        }
    }
}
