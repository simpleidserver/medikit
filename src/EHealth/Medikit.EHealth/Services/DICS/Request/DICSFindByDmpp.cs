// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.DICS.Request
{
    public class DICSFindByDmpp
    {
        [XmlElement(ElementName = "DeliveryEnvironment")]
        public string DeliveryEnvironment { get; set; }
        [XmlElement(ElementName = "Code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "CodeType")]
        public string CodeType { get; set; }

        public XElement Serialize()
        {
            var result = new XElement("FindByDmpp");
            if (!string.IsNullOrWhiteSpace(DeliveryEnvironment))
            {
                result.Add(new XElement("DeliveryEnvironment", DeliveryEnvironment));
            }

            if (!string.IsNullOrWhiteSpace(Code))
            {
                result.Add(new XElement("Code", Code));
            }

            if (!string.IsNullOrWhiteSpace(CodeType))
            {
                result.Add(new XElement("CodeType", CodeType));
            }

            return result;
        }
    }
}
