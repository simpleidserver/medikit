// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Response
{
    [XmlRoot(ElementName = "listRidsHistoryResult", Namespace = Constants.Namespaces.PRESCRIBER)]
    public class ListRidsHistoryResult
    {
        [XmlElement(ElementName = "status", Namespace = "")]
        public RecipeStatusResponse Status { get; set; }
        [XmlElement(ElementName = "items", Namespace = "")]
        public List<RidHistoryResult> PrescriptionHistories { get; set; }
        [XmlElement(ElementName = "hasMoreResults", Namespace = "")]
        public bool HasMoreResults { get; set; }

        public static ListRidsHistoryResult Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(ListRidsHistoryResult));
            ListRidsHistoryResult samlEnv = null;
            using (var reader = new StringReader(xml))
            {
                samlEnv = (ListRidsHistoryResult)serializer.Deserialize(reader);
            }

            return samlEnv;
        }
    }
}
