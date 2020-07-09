// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Request
{
    public class ListPrescriptionHistoryRequest
    {
        [XmlAttribute(AttributeName = "ProgramId")]
        public string ProgramId { get; set; }
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "IssueInstant")]
        public DateTime IssueInstant { get; set; }
        [XmlElement(ElementName = "SecuredListRidsHistoryRequest", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public SecuredContentType SecuredListRidsHistoryRequest { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.RECIPE + "ListRidsHistoryRequest",
                new XAttribute(XNamespace.Xmlns + "ns2", Constants.XMLNamespaces.RECIPE),
                new XAttribute(XNamespace.Xmlns + "ns3", Constants.XMLNamespaces.COMMONCORE),
                new XAttribute(XNamespace.Xmlns + "ns4", Constants.XMLNamespaces.COMMONPROTOCOL),
                new XAttribute("Id", Id),
                new XAttribute("IssueInstant", IssueInstant),
                new XAttribute("ProgramId", ProgramId));
            if (SecuredListRidsHistoryRequest != null)
            {
                result.Add(SecuredListRidsHistoryRequest.Serialize("SecuredListRidsHistoryRequest"));
            }

            return result;
        }
    }
}