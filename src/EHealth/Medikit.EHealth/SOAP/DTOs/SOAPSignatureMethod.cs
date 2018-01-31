// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPSignatureMethod
    {
        [XmlAttribute(AttributeName = "Algorithm")]
        public string Algorithm { get; set; }

        public XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.DS + "SignatureMethod",
                new XAttribute("Algorithm", Algorithm));
        }
    }
}
