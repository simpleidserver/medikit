// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Request
{
    [XmlRoot(ElementName = "createPrescriptionParam" /*, Namespace = Constants.Namespaces.RECIPE*/)]
    public class CreatePrescriptionParameter
    {
        [XmlElement(ElementName = "prescription")]
        public string Prescription { get; set; }
        [XmlElement(ElementName = "prescriptionType")]
        public string PrescriptionType { get; set; }
        [XmlElement(ElementName = "feedbackRequested")]
        public bool FeedbackRequested { get; set; }
        [XmlElement(ElementName = "keyId")]
        public string KeyId { get; set; }
        [XmlElement(ElementName = "symmKey")]
        public string SymmKey { get; set; }
        [XmlElement(ElementName = "prescriberLabel")]
        public string PrescriberLabel { get; set; }
        [XmlElement(ElementName = "expirationDate")]
        public string ExpirationDate { get; set; }
        [XmlElement(ElementName = "vision")]
        public string Vision { get; set; }
        [XmlElement(ElementName = "patientId")]
        public string PatientId { get; set; }

        public byte[] Serialize()
        {
            var serializer = new XmlSerializer(typeof(CreatePrescriptionParameter));
            using (var ms = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(ms))
                {
                    serializer.Serialize(writer, this);
                    return ms.ToArray();
                }
            }
        }
    }
}
