// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SOAP.DTOs;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.ETK.Request
{
    public class ETKGetRequestBody : SOAPBody
    {
        [XmlElement("GetEtkRequest", Namespace = Constants.Namespaces.ETK)]
        public ETKGetRequest Request { get; set; }

        public override XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.SOAPENV + "Body",
                Request.Serialize());
        }
    }
}
