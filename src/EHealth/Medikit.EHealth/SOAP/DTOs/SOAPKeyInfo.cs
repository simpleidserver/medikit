// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPKeyInfo
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "SecurityTokenReference", Namespace = Constants.Namespaces.WSSE)]
        public SOAPSecurityTokenReference SecurityTokenReference { get; set; }
        [XmlElement(ElementName = "X509Data")]
        public SOAPX509Data X509Data { get; set; }

        public XElement Serialize(bool ignoreDsNamespace)
        {
            var result = new XElement(Constants.XMLNamespaces.DS + "KeyInfo");
            if (!ignoreDsNamespace)
            {
                result.Add(new XAttribute(XNamespace.Xmlns + "ds", Constants.XMLNamespaces.DS));
            }

            if (!string.IsNullOrWhiteSpace(Id))
            {
                result.Add(new XAttribute("Id", Id));
            }

            if (SecurityTokenReference != null)
            {
                result.Add(SecurityTokenReference.Serialize());
            }

            if (X509Data != null)
            {
                result.Add(X509Data.Serialize());
            }

            return result;
        }
    }
}
