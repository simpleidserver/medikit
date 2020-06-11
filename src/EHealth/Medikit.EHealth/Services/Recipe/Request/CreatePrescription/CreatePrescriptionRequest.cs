// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Request
{
    [XmlRoot(ElementName = "CreatePrescriptionRequest")]
    public class CreatePrescriptionRequest
    {
        [XmlAttribute(AttributeName = "ProgramId")]
        public string ProgramId { get; set; }
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "IssueInstant")]
        public DateTime IssueInstant { get; set; }
        [XmlElement(ElementName = "SecuredCreatePrescriptionRequest", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public SecuredContentType SecuredCreatePrescriptionRequest { get; set; }
        [XmlElement(ElementName = "AdministrativeInformation")]
        public CreatePrescriptionAdministrativeInformationType AdministrativeInformation { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.RECIPE + "CreatePrescriptionRequest",
                new XAttribute(XNamespace.Xmlns + "ns2", Constants.XMLNamespaces.RECIPE),
                new XAttribute("Id", Id),
                new XAttribute("IssueInstant", IssueInstant),
                new XAttribute("ProgramId", ProgramId));
            if (SecuredCreatePrescriptionRequest != null)
            {
                result.Add(SecuredCreatePrescriptionRequest.Serialize("SecuredCreatePrescriptionRequest"));
            }

            if (AdministrativeInformation != null)
            {
                result.Add(AdministrativeInformation.Serialize());
            }

            return result;
        }
    }
}
