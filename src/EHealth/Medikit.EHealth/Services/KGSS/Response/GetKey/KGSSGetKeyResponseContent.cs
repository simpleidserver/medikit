// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.KGSS.Response.GetKey
{
    [XmlRoot(ElementName = "GetKeyResponseContent", Namespace = Constants.Namespaces.KGSS)]
    public class KGSSGetKeyResponseContent
    {
        [XmlElement(ElementName = "Key", Namespace = Constants.Namespaces.KGSS)]
        public string NewKey { get; set; }

        public static KGSSGetKeyResponseContent Deserialize(byte[] payload)
        {
            var serializer = new XmlSerializer(typeof(KGSSGetKeyResponseContent));
            KGSSGetKeyResponseContent samlEnv = null;
            using (var reader = new StringReader(Encoding.UTF8.GetString(payload)))
            {
                samlEnv = (KGSSGetKeyResponseContent)serializer.Deserialize(reader);
            }

            return samlEnv;
        }
    }
}
