// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Response
{
    public class CreatePrescriptionResponse
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "SecuredCreatePrescriptionResponse", Namespace = "", Form = XmlSchemaForm.None)]
        public SecuredGetPrescriptionResponse SecuredGetPrescriptionResponse { get; set; }
    }
}
