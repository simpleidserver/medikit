// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Extensions;
using Medikit.EHealth.SOAP.DTOs;
using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    [XmlRoot(ElementName = "Assertion", Namespace = Constants.Namespaces.SAML)]
    public class SAMLAssertion
    {
        [XmlAttribute(AttributeName = "AssertionID")]
        public string AssertionId { get; set; }
        [XmlAttribute(AttributeName = "IssueInstant")]
        public DateTime IssueInstant { get; set; }
        [XmlAttribute(AttributeName = "Issuer")]
        public string Issuer { get; set; }
        [XmlAttribute(AttributeName = "MajorVersion")]
        public int MajorVersion { get; set; }
        [XmlAttribute(AttributeName = "MinorVersion")]
        public int MinorVersion { get; set; }
        [XmlElement(ElementName = "Conditions")]
        public SAMLConditions Conditions { get; set; }
        [XmlElement(ElementName = "AuthenticationStatement")]
        public SAMLAuthenticationStatement AuthenticationStatement { get; set; }
        [XmlElement(ElementName = "AttributeStatement")]
        public SAMLAttributeStatement AttributeStatement { get; set; }
        [XmlElement(ElementName = "Signature", Namespace = Constants.Namespaces.DS)]
        public SOAPSignature Signature { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.SAML + "Assertion",
                // new XAttribute("xmlns", Constants.XMLNamespaces.SAML),
                new XAttribute("AssertionID", AssertionId),
                new XAttribute("IssueInstant", IssueInstant.ToUTCString()),
                new XAttribute("Issuer", Issuer),
                new XAttribute("MajorVersion", MajorVersion),
                new XAttribute("MinorVersion", MinorVersion));
            if(Conditions != null)
            {
                result.Add(Conditions.Serialize());
            }

            if (AuthenticationStatement != null)
            {
                result.Add(AuthenticationStatement.Serialize());
            }

            if (AttributeStatement != null)
            {
                result.Add(AttributeStatement.Serialize());
            }

            if (Signature != null)
            {
                result.Add(Signature.Serialize());
            }

            return result;
        }

        public static SAMLAssertion Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(SAMLAssertion));
            SAMLAssertion samlAss = null;
            using (var reader = new StringReader(xml))
            {
                samlAss = (SAMLAssertion)serializer.Deserialize(reader);
            }

            return samlAss;
        }
    }
}
