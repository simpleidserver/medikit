// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLSubjectConfirmationData
    {
        [XmlElement(ElementName = "Assertion")]
        public SAMLAssertion Assertion { get; set; }

        public XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.SAML + "SubjectConfirmationData",
                Assertion.Serialize());
        }
    }
}
