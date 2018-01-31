// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SOAP;
using Medikit.EHealth.SOAP.DTOs;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLResponseBody : SOAPBody
    {
        [XmlElement("Response", Namespace = Constants.Namespaces.SAMLP)]
        public SAMLResponse Response { get; set; }

        public override XElement Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
