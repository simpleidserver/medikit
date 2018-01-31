// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.KGSS.Request
{
    public class KGSSSealedNewKeyRequest
    {
        [XmlElement(ElementName = "SealedContent")]
        public string SealedContent { get; set; }

        public XElement Serialize()
        {
            return new XElement(Constants.XMLNamespaces.KGSS + "SealedNewKeyRequest",
                new XElement(Constants.XMLNamespaces.KGSS + "SealedContent", SealedContent));
        }
    }
}
