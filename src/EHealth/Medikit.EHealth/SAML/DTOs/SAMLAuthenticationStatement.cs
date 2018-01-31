// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLAuthenticationStatement
    {
        [XmlAttribute(AttributeName = "AuthenticationInstant")]
        public DateTime AuthenticationInstant { get; set; }
        [XmlAttribute(AttributeName = "AuthenticationMethod")]
        public string AuthenticationMethod { get; set; }
        [XmlElement(ElementName = "Subject")]
        public SAMLSubject Subject { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.SAML + "AuthenticationStatement",
                new XAttribute("AuthenticationInstant", AuthenticationInstant),
                new XAttribute("AuthenticationMethod", AuthenticationMethod));
            if (Subject != null)
            {
                result.Add(Subject.Serialize());
            }

            return result;
        }
    }
}
