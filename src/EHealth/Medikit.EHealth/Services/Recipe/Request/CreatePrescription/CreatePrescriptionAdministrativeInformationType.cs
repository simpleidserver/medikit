// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Request
{
    public class CreatePrescriptionAdministrativeInformationType
    {
        [XmlElement(ElementName = "PrescriptionType")]
        public string PrescriptionType { get; set; }
        [XmlElement(ElementName = "PrescriptionVersion")]
        public string PrescriptionVersion { get; set; }
        [XmlElement(ElementName = "ReferenceSourceVersion")]
        public string ReferenceSourceVersion { get; set; }
        [XmlElement(ElementName = "KeyIdentifier")]
        public string KeyIdentifier { get; set; }

        public XElement Serialize()
        {
            var result = new XElement("AdministrativeInformation");
            if (!string.IsNullOrWhiteSpace(PrescriptionType))
            {
                result.Add(new XElement("PrescriptionType", PrescriptionType));
            }

            if (!string.IsNullOrWhiteSpace(PrescriptionVersion))
            {
                result.Add(new XElement("PrescriptionVersion", PrescriptionVersion));
            }

            if (!string.IsNullOrWhiteSpace(ReferenceSourceVersion))
            {
                result.Add(new XElement("ReferenceSourceVersion", ReferenceSourceVersion));
            }

            if (!string.IsNullOrWhiteSpace(KeyIdentifier))
            {
                result.Add(new XElement("KeyIdentifier", KeyIdentifier));
            }

            return result;
        }
    }
}
