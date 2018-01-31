// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.KGSS
{
    public class KGSSStatus
    {
        [XmlElement(ElementName = "Code", Form = XmlSchemaForm.Unqualified)]
        public int Code { get; set; }
        [XmlElement(ElementName = "Message", Form = XmlSchemaForm.Unqualified)]
        public string Message { get; set; }
    }
}
