// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.DICS.Response
{
    public class DICSDmpp
    {
        [XmlAttribute(AttributeName = "Code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "CodeType")]
        public string CodeType { get; set; }
        [XmlAttribute(AttributeName = "DeliveryEnvironment")]
        public string DeliveryEnvironment { get; set; }
        [XmlAttribute(AttributeName = "ProductId")]
        public string ProductId { get; set; }
        [XmlAttribute(AttributeName = "StartDate")]
        public DateTime StartDate { get; set; }
        [XmlElement(ElementName = "Price")]
        public double Price { get; set; }
        [XmlElement(ElementName = "Reimbursable")]
        public bool Reimbursable { get; set; }
    }
}