// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.DICS.Response
{
    public class DICSAmpComponent
    {
        [XmlAttribute(AttributeName = "SequenceNr")]
        public int SequenceNr { get; set; }
        [XmlAttribute(AttributeName = "DateTime")]
        public DateTime StartDate { get; set; }
        [XmlAttribute(AttributeName = "VmpComponentCode")]
        public string VmpComponentCode { get; set; }
        [XmlElement(ElementName = "Scored")]
        public string Scored { get; set; }
        [XmlElement(ElementName = "SpecificDrugDevice")]
        public string SpecificDrugDevice { get; set; }
        [XmlArray(ElementName = "Name")]
        [XmlArrayItem(ElementName = "Text")]
        public List<DICSText> Name { get; set; }
        [XmlElement(ElementName = "PharmaceuticalForm")]
        public List<DICSPharmaceuticalForm> PharmaceuticalForms { get; set; }
        [XmlElement(ElementName = "RouterOfAdministration")]
        public List<DICSRouteOfAdministration> RouteOfAdministrations { get; set; }
        [XmlElement(ElementName = "RealActualIngredient")]
        public List<DICSRealActualIngredient> RealActualIngredients { get; set; }
    }
}
