// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.DICS.Response
{
    public class DICSPharmaceuticalForm
    {
        [XmlAttribute(AttributeName = "Code")]
        public string Code { get; set; }
        [XmlArray(ElementName = "Name")]
        [XmlArrayItem(ElementName = "Text")]
        public List<DICSText> Name { get; set; }
    }
}
