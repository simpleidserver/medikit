// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.KGSS.Response
{
    public class KGSSSealedResponse
    {
        [XmlElement(ElementName = "SealedContent")]
        public string SealedContent { get; set; }
    }
}
