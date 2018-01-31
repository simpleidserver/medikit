// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public abstract class SOAPBody
    {
        [XmlAttribute(AttributeName = "Id", Namespace = Constants.Namespaces.WSU, Form = XmlSchemaForm.Qualified)]
        public string Id { get; set; }

        public abstract XElement Serialize();
    }
}
