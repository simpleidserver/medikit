// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.DICS.Request
{
    public class DICSFindByPackage
    {
        [XmlElement(ElementName = "AnyNamePart")]
        public string AnyNamePart { get; set; }
        [XmlElement(ElementName = "AtcCode")]
        public string AtcCode { get; set; }
        [XmlElement(ElementName = "Commercialised")]
        public bool? Commercialised { get; set; }

        public XElement Serialize()
        {
            var result = new XElement("FindByPackage");
            if (!string.IsNullOrWhiteSpace(AnyNamePart))
            {
                result.Add(new XElement("AnyNamePart", AnyNamePart));
            }

            if (!string.IsNullOrWhiteSpace(AtcCode))
            {
                result.Add(new XElement("AtcCode", AtcCode));
            }

            if (Commercialised != null)
            {
                result.Add(new XElement("Commercialised", Commercialised.Value));
            }

            return result;
        }
    }
}
