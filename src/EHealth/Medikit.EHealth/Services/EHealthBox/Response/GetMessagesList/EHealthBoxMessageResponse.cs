// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.EHealthBox.Response
{
    public class EHealthBoxMessageResponse
    {
        [XmlElement(ElementName = "MessageId")]
        public string MessageId { get; set; }
        [XmlElement(ElementName = "Destination")]
        public EHealthBoxDestinationResponse Destination { get; set; }
        [XmlElement(ElementName = "Sender")]
        public EHealthBoxSenderResponse Sender { get; set; }
        [XmlElement(ElementName = "MessageInfo")]
        public EHealthBoxMessageInfoResponse MessageInfo { get; set; }
        [XmlElement(ElementName = "ContentInfo")]
        public EHealthBoxContentInfoResponse ContentInfo { get; set; }
        [XmlElement(ElementName = "ContentSpecification")]
        public EHealthBoxContentSpecificationResponse ContentSpecification { get; set; }
    }
}
