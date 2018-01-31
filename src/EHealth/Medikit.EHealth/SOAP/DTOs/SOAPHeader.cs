// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPHeader
    {
        [XmlElement(ElementName = "Security", Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
        public SOAPSecurity Security { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.SOAPENV + "Header");
            if (Security != null)
            {
                result.Add(Security.Serialize());
            }

            return result;
        }
    }
}