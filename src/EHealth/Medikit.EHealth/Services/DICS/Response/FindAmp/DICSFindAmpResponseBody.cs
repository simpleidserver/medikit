// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SOAP.DTOs;
using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.DICS.Response
{
    public class DICSFindAmpResponseBody : SOAPBody
    {
        [XmlElement(ElementName = "FindAmpResponse", Namespace = Constants.Namespaces.DICSV5)]
        public DICSFindAmpResponse Response { get; set; }

        public override XElement Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
