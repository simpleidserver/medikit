// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Request
{
    [XmlRoot(ElementName = "GetValidationPropertiesRequest")]
    public class GetValidationPropertiesRequest
    {
        [XmlAttribute(AttributeName = "ProgramId")]
        public string ProgramId { get; set; }
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "IssueInstant")]
        public DateTime IssueInstant { get; set; }
        [XmlElement(ElementName = "SecuredGetValidationPropertiesRequest", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public SecuredContentType SecuredCreatePrescriptionRequest { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.RECIPE + "GetValidationPropertiesRequest",
                new XAttribute(XNamespace.Xmlns + "ns2", Constants.XMLNamespaces.RECIPE),
                new XAttribute(XNamespace.Xmlns + "ns3", Constants.XMLNamespaces.COMMONCORE),
                new XAttribute(XNamespace.Xmlns + "ns4", Constants.XMLNamespaces.COMMONPROTOCOL),
                new XAttribute("Id", Id),
                new XAttribute("IssueInstant", IssueInstant),
                new XAttribute("ProgramId", ProgramId));
            if (SecuredCreatePrescriptionRequest != null)
            {
                result.Add(SecuredCreatePrescriptionRequest.Serialize("SecuredGetValidationPropertiesRequest"));
            }

            return result;
        }
    }
}
