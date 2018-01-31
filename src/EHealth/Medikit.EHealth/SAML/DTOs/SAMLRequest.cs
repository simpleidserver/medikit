// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Extensions;
using Medikit.EHealth.SOAP.DTOs;
using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLRequest 
    {
        [XmlAttribute(AttributeName = "IssueInstant")]
        public DateTime IssueInstant { get; set; }
        [XmlAttribute(AttributeName = "MajorVersion")]
        public int MajorVersion { get; set; }
        [XmlAttribute(AttributeName = "MinorVersion")]
        public int MinorVersion { get; set; }
        [XmlAttribute(AttributeName = "RequestID")]
        public string RequestId { get; set; }
        [XmlElement(ElementName = "AttributeQuery")]
        public SAMLAttributeQuery AttributeQuery { get; set; }
        [XmlElement(ElementName = "Signature")]
        public SOAPSignature Signature { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.SAMLP + "Request",
                new XAttribute(XNamespace.Xmlns + "samlp", Constants.XMLNamespaces.SAMLP),
                new XAttribute(XNamespace.Xmlns + "ds", Constants.XMLNamespaces.DS),
                new XAttribute(XNamespace.Xmlns + "saml", Constants.XMLNamespaces.SAML),
                new XAttribute("IssueInstant", IssueInstant.ToUTCString()),
                new XAttribute("MajorVersion", MajorVersion),
                new XAttribute("MinorVersion", MinorVersion),
                new XAttribute("RequestID", RequestId));
            if (Signature != null)
            {
                result.Add(Signature.Serialize());
            }

            if (AttributeQuery != null)
            {
                result.Add(AttributeQuery.Serialize());
            }

            return result;
        }
    }
}
