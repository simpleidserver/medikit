// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.ETK.Request
{
    public class ETKGetRequest
    {
        [XmlElement("SearchCriteria")]
        public ETKSearchCriteria SearchCriteria { get; set; }

        public XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.ETK + "GetEtkRequest",
                new XAttribute("xmlns", Constants.XMLNamespaces.ETK),
                SearchCriteria.Serialize());
        }
    }
}
