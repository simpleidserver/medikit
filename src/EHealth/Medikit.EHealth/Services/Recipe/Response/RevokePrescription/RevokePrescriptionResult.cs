// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.IO;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Response
{
    [XmlRoot(ElementName = "revokePrescriptionResult", Namespace = Constants.Namespaces.PRESCRIBER)]
    public class RevokePrescriptionResult
    {
        [XmlElement(ElementName = "status", Namespace = "")]
        public RecipeStatusResponse Status { get; set; }

        public static RevokePrescriptionResult Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(RevokePrescriptionResult));
            RevokePrescriptionResult samlEnv = null;
            using (var reader = new StringReader(xml))
            {
                samlEnv = (RevokePrescriptionResult)serializer.Deserialize(reader);
            }

            return samlEnv;
        }
    }
}
