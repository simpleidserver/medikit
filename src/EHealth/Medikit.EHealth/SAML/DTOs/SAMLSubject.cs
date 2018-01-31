// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLSubject
    {
        [XmlElement(ElementName = "NameIdentifier")]
        public SAMLNameIdentifier NameIdentifier { get; set; }
        [XmlElement(ElementName = "SubjectConfirmation")]
        public SAMLSubjectConfirmation SubjectConfirmation { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.SAML + "Subject");
            if (NameIdentifier != null)
            {
                result.Add(NameIdentifier.Serialize());
            }

            if (SubjectConfirmation != null)
            {
                result.Add(SubjectConfirmation.Serialize());
            }

            return result;
        }
    }
}
