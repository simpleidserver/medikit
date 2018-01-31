// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SAML.DTOs;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPSecurity
    {
        [XmlAttribute(AttributeName = "mustUnderstand")]
        public int MustUnderstand { get; set; }
        [XmlElement(ElementName = "Timestamp", Namespace = Constants.Namespaces.WSU)]
        public SOAPTimestamp Timestamp { get; set; }
        [XmlElement(ElementName = "BinarySecurityToken", Namespace = Constants.Namespaces.WSSE)]
        public SOAPBinarySecurityToken BinarySecurityToken { get; set; }
        [XmlElement(ElementName = "Signature", Namespace = Constants.Namespaces.DS)]
        public SOAPSignature Signature { get; set; }
        [XmlElement(ElementName = "Assertion", Namespace = Constants.Namespaces.SAML)]
        public SAMLAssertion Assertion { get; set; }

        public virtual XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.WSSE + "Security",
                new XAttribute(XNamespace.Xmlns + "wsse", Constants.XMLNamespaces.WSSE),
                new XAttribute(XNamespace.Xmlns + "wsu", Constants.XMLNamespaces.WSU),
                new XAttribute(Constants.XMLNamespaces.SOAPENV + "mustUnderstand", "1"),
                Timestamp.Serialize());
            if (BinarySecurityToken != null)
            {
                result.Add(BinarySecurityToken.Serialize());
            }

            if (Assertion != null)
            {
                result.Add(Assertion.Serialize());
            }

            if (Signature != null)
            {
                result.Add(Signature.Serialize());
            }

            return result;
        }
    }
}