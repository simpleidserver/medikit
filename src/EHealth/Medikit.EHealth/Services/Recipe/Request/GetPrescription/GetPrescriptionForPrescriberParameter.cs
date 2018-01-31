// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Request
{
    [XmlRoot(ElementName = "getPrescriptionForPrescriberParam")]
    public class GetPrescriptionForPrescriberParameter
    {
        [XmlElement(ElementName = "rid")]
        public string Rid { get; set; }
        [XmlElement(ElementName = "symmKey")]
        public string SymmKey { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.PRESCRIBER + "getPrescriptionForPrescriberParam",
                new XAttribute(XNamespace.Xmlns + "ns2", Constants.Namespaces.PRESCRIBER),
                new XAttribute(XNamespace.Xmlns + "ns3", Constants.Namespaces.PATIENT),
                new XAttribute(XNamespace.Xmlns + "ns4", Constants.Namespaces.EXECUTOR),
                new XElement("rid", Rid),
                new XElement("symmKey", SymmKey));
            return result;
        }
    }
}
