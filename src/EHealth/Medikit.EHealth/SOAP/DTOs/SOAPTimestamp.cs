// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Extensions;
using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPTimestamp
    {
        [XmlAttribute("Id", Namespace = Constants.Namespaces.WSU, Form = XmlSchemaForm.Qualified)]
        public string Id { get; set; }
        [XmlElement("Created")]
        public DateTime Created { get; set; }
        [XmlElement("Expires")]
        public DateTime Expires { get; set; }

        public XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.WSU + "Timestamp",
                new XAttribute(Constants.XMLNamespaces.WSU + "Id", Id),
                new XElement(Constants.XMLNamespaces.WSU + "Created", Created.ToUTCString()),
                new XElement(Constants.XMLNamespaces.WSU + "Expires", Expires.ToUTCString()));
        }
    }
}
