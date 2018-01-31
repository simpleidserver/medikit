// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SOAP.DTOs;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.DICS.Request
{
    public class DICSFindAmpRequestBody : SOAPBody
    {
        [XmlElement(ElementName = "FindAmpRequest", Namespace = Constants.Namespaces.DICSV5)]
        public DICSFindAmpRequest Request { get; set; }

        public override XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.SOAPENV + "Body",
                new XAttribute(XNamespace.Xmlns + "wsu", Constants.XMLNamespaces.WSU),
                new XAttribute(Constants.XMLNamespaces.WSU + "Id", Id),
                Request.Serialize());
        }
    }
}
