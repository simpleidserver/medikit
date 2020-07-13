// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SOAP.DTOs;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.EHealthBox.Response
{
    public class EHealthBoxGetMessagesListResponseBody : SOAPBody
    {
        [XmlElement(ElementName = "GetMessagesListResponse", Namespace = Constants.Namespaces.EHEALTHBOX_CONSULTATION, Form = XmlSchemaForm.Qualified)]
        public EHealthBoxGetMessagesListResponse GetMessagesListResponse { get; set; }

        public override XElement Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
