// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;

namespace Medikit.EHealth.Services.KGSS
{
    public class CredentialType
    {
        /// <summary>
        /// The namespace of the Credential attribute, urn:be:fgov:ehealth:certified‐namespace
        /// </summary>
        public string Namespace { get; set; }
        /// <summary>
        /// The name of the Credential attribute, urn:be:fgov:ehealth:doctor‐nihii
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The value of the Credential attribut, 74042015445
        /// </summary>
        public string Value { get; set; }

        public XElement Serialize(string elementName)
        {
            return new XElement(Constants.XMLNamespaces.KGSS + elementName,
                new XElement(Constants.XMLNamespaces.KGSS + "Namespace", Namespace),
                new XElement(Constants.XMLNamespaces.KGSS + "Name", Name),
                new XElement(Constants.XMLNamespaces.KGSS + "Value", Value));
        }
    }
}
