// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Response
{
    public class SecuredGetPrescriptionResponse
    {
        [XmlElement(ElementName = "SecuredContent", Form = XmlSchemaForm.None)]
        public string SecuredContent { get; set; }
    }
}
