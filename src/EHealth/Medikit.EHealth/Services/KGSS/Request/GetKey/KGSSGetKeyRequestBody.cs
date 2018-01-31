// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SOAP.DTOs;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.KGSS.Request
{
    /// <summary>
    /// This method will create and store a new symmetrical encryption key.
    /// It is used by the sender. The key and its identifier are returned to the requestor.
    /// </summary>
    public class KGSSGetKeyRequestBody : SOAPBody
    {
        [XmlElement(ElementName = "GetKeyRequest", Namespace = Constants.Namespaces.KGSS)]
        public KGSSGetKeyRequest Request { get; set; }

        public override XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.SOAPENV + "Body",
                new XAttribute(XNamespace.Xmlns + "wsu", Constants.XMLNamespaces.WSU),
                new XAttribute(Constants.XMLNamespaces.WSU + "Id", Id),
                Request.Serialize());
        }
    }
}
