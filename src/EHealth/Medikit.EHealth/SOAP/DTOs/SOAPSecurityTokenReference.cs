// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SAML;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPSecurityTokenReference
    {
        [XmlAttribute(AttributeName = "Id", Namespace = Constants.Namespaces.WSU, Form = XmlSchemaForm.Qualified)]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "TokenType", Namespace = Constants.Namespaces.WSSE11, Form = XmlSchemaForm.Qualified)]
        public string TokenType { get; set; }

        [XmlElement(ElementName = "Reference")]
        public SOAPKeyInfoReference Reference { get; set; }

        [XmlElement(ElementName = "KeyIdentifier")]
        public SOAPKeyIdentifier KeyIdentifier { get; set; }

        public virtual XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.WSSE + "SecurityTokenReference",
                new XAttribute(XNamespace.Xmlns + "wsse11", Constants.XMLNamespaces.WSSE11));

            if (!string.IsNullOrWhiteSpace(TokenType))
            {
                result.Add(new XAttribute(Constants.XMLNamespaces.WSSE11 + "TokenType", TokenType));
            }

            result.Add(new XAttribute(Constants.XMLNamespaces.WSU + "Id", Id));
            if (Reference != null)
            {
                result.Add(Reference.Serialize());
            }

            if (KeyIdentifier != null)
            {
                result.Add(KeyIdentifier.Serialize());
            }

            return result;
        }
    }
}