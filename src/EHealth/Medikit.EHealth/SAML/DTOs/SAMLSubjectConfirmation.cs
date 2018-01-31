// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SOAP;
using Medikit.EHealth.SOAP.DTOs;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLSubjectConfirmation
    {
        [XmlElement(ElementName = "ConfirmationMethod")]
        public string ConfirmationMethod { get; set; }
        [XmlElement(ElementName = "SubjectConfirmationData")]
        public SAMLSubjectConfirmationData SubjectConfirmationData { get; set; }
        [XmlElement(ElementName = "KeyInfo", Namespace = Constants.Namespaces.DS)]
        public SOAPKeyInfo KeyInfo { get; set; }   

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.SAML + "SubjectConfirmation",
                new XElement(Constants.XMLNamespaces.SAML + "ConfirmationMethod", ConfirmationMethod));
            if (SubjectConfirmationData != null)
            {
                result.Add(SubjectConfirmationData.Serialize());
            }

            if (KeyInfo != null)
            {
                result.Add(KeyInfo.Serialize(false));
            }

            return result;
        }
    }
}
