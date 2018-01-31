// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.IO;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Response
{
    [XmlRoot(ElementName = "getPrescriptionForPrescriberResult", Namespace = Constants.Namespaces.PRESCRIBER)]
    public class GetPrescriptionForPrescriberResult
    {
        [XmlElement(ElementName = "status", Namespace = "")]
        public RecipeStatusResponse Status { get; set; }
        [XmlElement(ElementName = "rid", Namespace = "")]
        public string Rid { get; set; }
        [XmlElement(ElementName = "creationDate", Namespace = "")]
        public DateTime CreationDate { get; set; }
        [XmlElement(ElementName = "patientId", Namespace = "")]
        public string PatientId { get; set; }
        [XmlElement(ElementName = "feedbackAllowed", Namespace = "")]
        public bool FeedbackAllowed { get; set; }
        [XmlElement(ElementName = "prescription", Namespace = "")]
        public string Prescription { get; set; }
        [XmlElement(ElementName = "encryptionKeyId", Namespace = "")]
        public string EncryptionKeyId { get; set; }
        [XmlElement(ElementName = "expirationDate", Namespace = "")]
        public DateTime ExpirationDate { get; set; }

        public static GetPrescriptionForPrescriberResult Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(GetPrescriptionForPrescriberResult));
            GetPrescriptionForPrescriberResult samlEnv = null;
            using (var reader = new StringReader(xml))
            {
                samlEnv = (GetPrescriptionForPrescriberResult)serializer.Deserialize(reader);
            }

            return samlEnv;
        }
    }
}
