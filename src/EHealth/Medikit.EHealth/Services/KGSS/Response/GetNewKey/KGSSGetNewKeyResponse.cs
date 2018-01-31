// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.KGSS.Response
{
    public class KGSSGetNewKeyResponse
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "Status", Form = XmlSchemaForm.Unqualified)]
        public KGSSStatus Status { get; set; }
        [XmlElement(ElementName = "SealedNewKeyResponse", Namespace = Constants.Namespaces.KGSS)]
        public KGSSSealedResponse SealedNewKeyResponse { get; set; }
    }
}
