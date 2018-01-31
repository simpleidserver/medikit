// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLAttributeStatement
    {
        [XmlElement(ElementName = "Subject", Namespace = "urn:oasis:names:tc:SAML:1.0:assertion")]
        public SAMLSubject Subject { get; set; }
        [XmlElement(ElementName = "Attribute", Namespace = "urn:oasis:names:tc:SAML:1.0:assertion")]
        public List<SAMLAttribute> Attribute { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.SAML + "AttributeStatement",
                Subject.Serialize());
            foreach(var attr in Attribute)
            {
                result.Add(attr.Serialize());
            }

            return result;
        }
    }
}