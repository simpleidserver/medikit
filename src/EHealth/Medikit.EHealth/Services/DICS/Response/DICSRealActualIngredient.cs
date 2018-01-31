// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.DICS.Response
{
    public class DICSRealActualIngredient
    {
        [XmlAttribute(AttributeName = "Rank")]
        public int Rank { get; set; }
        [XmlAttribute(AttributeName = "StartDate")]
        public DateTime StartDate { get; set; }
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }
    }
}
