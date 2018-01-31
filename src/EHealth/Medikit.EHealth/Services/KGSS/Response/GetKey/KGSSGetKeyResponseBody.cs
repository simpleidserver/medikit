// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SOAP.DTOs;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.KGSS.Response
{
    public class KGSSGetKeyResponseBody : SOAPBody
    {
        [XmlElement("GetKeyResponse", Namespace = Constants.Namespaces.KGSS)]
        public KGSSGetKeyResponse GetKeyResponse { get; set; }

        public override XElement Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
