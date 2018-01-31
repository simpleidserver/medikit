// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.ETK.Request
{
    public class ETKSearchCriteria
    {
        [XmlElement("Identifier")]
        public ETKIdentifier Identifier { get; set; }

        public XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.ETK + "SearchCriteria", Identifier.Serialize());
        }
    }
}
