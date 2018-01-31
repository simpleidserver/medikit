// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SOAP.DTOs;
using System.Xml.Linq;

namespace Medikit.EHealth.SAML.DTOs
{
    public class SAMLSecurity : SOAPSecurity
    {
        public SAMLAssertion Assertion { get; set; }

        public override XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.WSSE + "Security");
            if (Assertion != null)
            {
                result.Add(Assertion.Serialize());
            }

            return result;
        }
    }
}
