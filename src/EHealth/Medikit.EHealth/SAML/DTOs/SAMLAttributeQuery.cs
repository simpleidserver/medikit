// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLAttributeQuery
    {
        [XmlElement(ElementName = "Subject", Namespace = Constants.Namespaces.SAML)]
        public SAMLSubject Subject { get; set; }
        [XmlElement(ElementName = "AttributeDesignator", Namespace = Constants.Namespaces.SAML)]
        public List<SAMLAttributeDesignator> AttributeDesignator { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.SAMLP + "AttributeQuery");
            if (Subject != null)
            {
                result.Add(Subject.Serialize());
            }

            if (AttributeDesignator != null)
            {
                foreach (var attr in AttributeDesignator)
                {
                    result.Add(attr.Serialize());
                }
            }

            return result;
        }
    }
}
