// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using Medikit.EHealth.SOAP.DTOs;

namespace Medikit.EHealth.Services.EHealthBox.Response
{
    public class EHealthBoxSendMessageResponseBody : SOAPBody
    {
        [XmlElement(ElementName = "SendMessageResponse", Namespace = Constants.Namespaces.EHEALTHBOX_PUBLICATION, Form = XmlSchemaForm.Qualified)]
        public EHealthBoxSendMessageResponse SendMessageResponse { get; set; }

        public override XElement Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
