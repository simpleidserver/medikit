// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.IO;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Response
{
    [XmlRoot(ElementName = "createPrescriptionResult", Namespace = Constants.Namespaces.PRESCRIBER)]
    public class CreatePrescriptionResult
    {
        [XmlElement(ElementName = "status", Namespace = "")]
        public RecipeStatusResponse Status { get; set; }
        [XmlElement(ElementName = "rid", Namespace = "")]
        public string RID { get; set; }

        public static CreatePrescriptionResult Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(CreatePrescriptionResult));
            CreatePrescriptionResult samlEnv = null;
            using (var reader = new StringReader(xml))
            {
                samlEnv = (CreatePrescriptionResult)serializer.Deserialize(reader);
            }

            return samlEnv;
        }
    }
}
