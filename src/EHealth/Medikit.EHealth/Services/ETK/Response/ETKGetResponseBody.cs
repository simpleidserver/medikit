// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SOAP.DTOs;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.ETK.Response
{
    public class ETKGetResponseBody : SOAPBody
    {
        [XmlElement("GetEtkResponse", Namespace = Constants.Namespaces.ETK)]
        public ETKGetResponse GetETKResponse { get; set; }

        public override XElement Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
