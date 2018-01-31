// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.DICS.Request
{
    public class DICSFindAmpRequest
    {
        public DICSFindAmpRequest()
        {
        }

        [XmlAttribute(AttributeName = "IssueInstant")]
        public DateTime IssueInstant { get; set; }
        [XmlElement(ElementName = "FindByProduct")]
        public DICSFindByProduct FindByProduct { get; set; }
        [XmlElement(ElementName = "FindByPackage")]
        public DICSFindByPackage FindByPackage { get; set; }
        [XmlElement(ElementName = "FindByDmpp")]
        public DICSFindByDmpp FindByDmpp { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.DICSV5 + "FindAmpRequest",
                new XAttribute(XNamespace.Xmlns + "ns2", Constants.XMLNamespaces.DICSV5),
                new XAttribute(XNamespace.Xmlns + "ns3", Constants.XMLNamespaces.COMMONCORE),
                new XAttribute(XNamespace.Xmlns + "ns4", Constants.XMLNamespaces.COMMONPROTOCOL),
                new XAttribute("IssueInstant", IssueInstant));
            if (FindByProduct != null)
            {
                result.Add(FindByProduct.Serialize());
            }

            if (FindByPackage != null)
            {
                result.Add(FindByPackage.Serialize());
            }

            if (FindByDmpp != null)
            {
                result.Add(FindByDmpp.Serialize());
            }

            return result;
        }
    }
}
