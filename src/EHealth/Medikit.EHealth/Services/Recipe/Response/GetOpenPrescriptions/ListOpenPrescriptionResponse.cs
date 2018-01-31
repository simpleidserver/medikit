// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Response
{
    public class ListOpenPrescriptionResponse
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "SecuredListOpenRidsResponse", Namespace = "", Form = XmlSchemaForm.None)]
        public SecuredListOpenRidsResponse SecuredListOpenRidsResponse { get; set; }
    }
}
