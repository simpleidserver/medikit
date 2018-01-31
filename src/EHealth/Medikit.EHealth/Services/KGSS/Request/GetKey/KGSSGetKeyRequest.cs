// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.KGSS.Request
{
    /// <summary>
    /// This method will create and store a new symmetrical encryption key.
    /// It is used by the sender. The key and its identifier are returned to the requestor.
    /// </summary>
    public class KGSSGetKeyRequest
    {
        [XmlElement(ElementName = "SealedKeyRequest")]
        public KGSSSealedKeyRequest SealedKeyRequest { get; set; }

        public XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.KGSS + "GetKeyRequest",
                new XAttribute("xmlns", Constants.XMLNamespaces.KGSS),
                SealedKeyRequest.Serialize());
        }
    }
}
