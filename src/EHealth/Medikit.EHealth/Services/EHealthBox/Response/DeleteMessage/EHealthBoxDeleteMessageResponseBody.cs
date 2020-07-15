// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using Medikit.EHealth.SOAP.DTOs;

namespace Medikit.EHealth.Services.EHealthBox.Response
{
    public class EHealthBoxDeleteMessageResponseBody : SOAPBody
    {
        [XmlElement(ElementName = "DeleteMessageResponse", Namespace = Constants.Namespaces.EHEALTHBOX_CONSULTATION, Form = XmlSchemaForm.Qualified)]
        public EHealthBoxDeleteMessageResponse DeleteMessageResponse { get; set; }

        public override XElement Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
