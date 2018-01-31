// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.IO;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Response
{
    [XmlRoot(ElementName = "listOpenRidsResult", Namespace = Constants.Namespaces.PRESCRIBER)]
    public class ListOpenRidsResult
    {
        [XmlElement(ElementName = "status", Namespace = "")]
        public RecipeStatusResponse Status { get; set; }
        [XmlElement(ElementName = "prescriptions", Namespace = "")]
        public List<string> Prescriptions { get; set; }
        [XmlElement(ElementName = "hasMoreResults", Namespace = "")]
        public bool HasMoreResults { get; set; }

        public static ListOpenRidsResult Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(ListOpenRidsResult));
            ListOpenRidsResult samlEnv = null;
            using (var reader = new StringReader(xml))
            {
                samlEnv = (ListOpenRidsResult)serializer.Deserialize(reader);
            }

            return samlEnv;
        }
    }
}
