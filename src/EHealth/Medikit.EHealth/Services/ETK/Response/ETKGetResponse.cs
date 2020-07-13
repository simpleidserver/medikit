// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Common;
using System.Xml.Serialization;

namespace Medikit.EHealth.ETK.Response
{
    public class ETKGetResponse
    {
        [XmlElement(ElementName = "Status", Namespace = Constants.Namespaces.CORE)]
        public EHealthStatus Status { get; set; }
        [XmlElement(ElementName = "GivenSearchCriteria")]
        public ETKGivenSearchCriteria GivenSearchCriteria { get; set; }
        [XmlElement(ElementName = "ETK")]
        public string ETK { get; set; }
        [XmlElement(ElementName = "Error")]
        public ETKError Error { get; set; }
    }
}
