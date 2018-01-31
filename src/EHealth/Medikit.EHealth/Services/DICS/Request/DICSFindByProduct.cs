// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.DICS.Request
{
    public class DICSFindByProduct
    {
        [XmlElement(ElementName = "AnyNamePart")]
        public string AnyNamePart { get; set; }

        public XElement Serialize()
        {
            var result = new XElement("FindByProduct",
                new XElement("AnyNamePart", AnyNamePart));
            return result;
        }
    }
}
