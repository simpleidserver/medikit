// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPFault
    {
        [XmlElement("faultcode", Form = XmlSchemaForm.Unqualified)]
        public string FaultCode { get; set; }
        [XmlElement("faultstring", Form = XmlSchemaForm.Unqualified)]
        public string FaultString { get; set; }
    }
}
