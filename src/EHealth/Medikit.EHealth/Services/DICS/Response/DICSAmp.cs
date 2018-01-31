// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.DICS.Response
{
    public class DICSAmp
    {
        [XmlAttribute(AttributeName = "Code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "StartDate")]
        public DateTime StartDate { get; set; }
        [XmlAttribute(AttributeName = "VmpCode")]
        public string VmpCode { get; set; }
        [XmlArray(ElementName = "Name")]
        [XmlArrayItem(ElementName = "Text")]
        public List<DICSText> Name { get; set; }
        [XmlElement(ElementName = "BlackTriangle")]
        public string BlackTriangle { get; set; }
        [XmlElement(ElementName = "MedicineType")]
        public string MedicineType { get; set; }
        [XmlElement(ElementName = "OfficialName")]
        public string OfficialName { get; set; }
        [XmlArray(ElementName = "AbbreviatedName")]
        [XmlArrayItem(ElementName = "Text")]
        public List<DICSText> AbbreviatedName { get; set; }
        [XmlArray(ElementName = "ProprietarySuffix")]
        [XmlArrayItem(ElementName = "Text")]
        public List<DICSText> ProprietarySuffix { get; set; }
        [XmlArray(ElementName = "PrescriptionName")]
        [XmlArrayItem(ElementName = "Text")]
        public List<DICSText> PrescriptionName { get; set; }
        [XmlElement(ElementName = "CompanyActorNr")]
        public string CompanyActorNr { get; set; }
        [XmlElement(ElementName = "AmpComponent")]
        public List<DICSAmpComponent> AmpComponents { get; set; }
        [XmlElement(ElementName = "Ampp")]
        public List<DICSAmpp> AmppLst { get; set; }
    }
}
