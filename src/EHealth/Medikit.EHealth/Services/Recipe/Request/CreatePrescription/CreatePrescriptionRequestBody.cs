// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SOAP.DTOs;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Request
{
    public class CreatePrescriptionRequestBody : SOAPBody
    {
        [XmlElement(ElementName = "CreatePrescriptionRequest", Namespace = Constants.Namespaces.RECIPE)]
        public CreatePrescriptionRequest Request { get; set; }

        public override XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.SOAPENV + "Body",
                new XAttribute(XNamespace.Xmlns + "wsu", Constants.XMLNamespaces.WSU),
                new XAttribute(Constants.XMLNamespaces.WSU + "Id", Id),
                Request.Serialize());
        }
    }
}