// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.DICS.Response
{
    public class DICSAmpp
    {
        [XmlAttribute(AttributeName = "CtiExtended")]
        public string CtiExtended { get; set; }
        [XmlAttribute(AttributeName = "StartDate")]
        public DateTime StartDate { get; set; }
        [XmlElement(ElementName = "Orphan")]
        public bool Orphan { get; set; }
        [XmlArray(ElementName = "LeafletUrl")]
        [XmlArrayItem(ElementName = "Text")]
        public List<DICSText> LeafletUrls { get; set; }
        [XmlArray(ElementName = "SpcUrl")]
        [XmlArrayItem(ElementName = "Text")]
        public List<DICSText> SpcUrls { get; set; }
        [XmlElement(ElementName = "ParallelCircuit")]
        public string ParallelCircuit { get; set; }
        [XmlElement(ElementName = "PackDisplayValue")]
        public string PackDisplayValue { get; set; }
        [XmlElement(ElementName = "AuthorisationNr")]
        public string AuthorisationNr { get; set; }
        [XmlArray(ElementName = "AbbreviatedName")]
        [XmlArrayItem(ElementName = "Text")]
        public List<DICSText> AbbreviatedNames { get; set; }
        [XmlArray(ElementName = "PrescriptionName")]
        [XmlArrayItem(ElementName = "Text")]
        public List<DICSText> PrescriptionNames { get; set; }
        [XmlArray(ElementName = "CrmUrl")]
        [XmlArrayItem(ElementName = "Text")]
        public List<DICSText> CrmUrls { get; set; }
        [XmlElement(ElementName = "ExFactoryPrice")]
        public double ExFactoryPrice { get; set; }
        [XmlElement(ElementName = "Atc")]
        public List<DICSAtc> Atc { get; set; }
        [XmlElement(ElementName = "Status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "Commercialization")]
        public DICSCommercialization Commercialization { get; set; }
        [XmlElement(ElementName = "Dmpp")]
        public List<DICSDmpp> DmppLst { get; set; }
    }
}
