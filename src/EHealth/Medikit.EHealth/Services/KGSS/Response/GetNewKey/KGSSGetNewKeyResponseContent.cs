// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.KGSS.Response
{
    [XmlRoot(ElementName = "GetNewKeyResponseContent", Namespace = Constants.Namespaces.KGSS)]
    public class KGSSGetNewKeyResponseContent
    {
        [XmlElement(ElementName = "NewKeyIdentifier", Namespace = Constants.Namespaces.KGSS)]
        public string NewKeyIdentifier { get; set; }
        [XmlElement(ElementName = "NewKey", Namespace = Constants.Namespaces.KGSS)]
        public string NewKey { get; set; }


        public static KGSSGetNewKeyResponseContent Deserialize(byte[] payload)
        {
            var serializer = new XmlSerializer(typeof(KGSSGetNewKeyResponseContent));
            KGSSGetNewKeyResponseContent samlEnv = null;
            using (var reader = new StringReader(Encoding.UTF8.GetString(payload)))
            {
                samlEnv = (KGSSGetNewKeyResponseContent)serializer.Deserialize(reader);
            }

            return samlEnv;
        }
    }
}
