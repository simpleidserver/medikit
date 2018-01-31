// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPSignature
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "SignedInfo")]
        public SOAPSignedInfo SignedInfo { get; set; }
        [XmlElement(ElementName = "SignatureValue")]
        public string SignatureValue { get; set; }
        [XmlElement(ElementName = "KeyInfo")]
        public SOAPKeyInfo KeyInfo { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.DS + "Signature",
                new XAttribute(XNamespace.Xmlns + "ds", Constants.XMLNamespaces.DS));
            if (!string.IsNullOrWhiteSpace(Id))
            {
                result.Add(new XAttribute("Id", Id));
            }

            if (SignedInfo != null)
            {
                result.Add(SignedInfo.Serialize());
            }

            if (!string.IsNullOrWhiteSpace(SignatureValue))
            {
                result.Add(new XElement(Constants.XMLNamespaces.DS + "SignatureValue", SignatureValue));
            }

            if (KeyInfo != null)
            {
                result.Add(KeyInfo.Serialize(true));
            }

            return result;
        }
    }
}
