// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPX509Data
    {
        [XmlElement(ElementName = "X509Certificate", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public string X509Certificate { get; set; }

        public XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.DS + "X509Data",
                new XElement(Constants.XMLNamespaces.DS + "X509Certificate", X509Certificate));
        }
    }
}
