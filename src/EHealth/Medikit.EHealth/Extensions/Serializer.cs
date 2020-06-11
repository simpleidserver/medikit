// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Medikit.EHealth.Extensions
{
    public static class Serializer
    {
        public static byte[] SerializeToByte<T>(this T elt, bool omitXmlDeclaration, bool indent)
        {
            var serializer = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = omitXmlDeclaration,
                Indent = indent
            };
            byte[] result = null;
            using (var ms = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(ms, settings))
                {
                    if (!omitXmlDeclaration)
                    {
                        writer.WriteStartDocument(true);
                    }

                    serializer.Serialize(writer, elt);
                    result = ms.ToArray();
                }
            }

            return result;
        }

        public static string SerializeToString<T>(this T elt, bool omitXmlDeclaration, bool indent = false)
        {
            var payload = elt.SerializeToByte(omitXmlDeclaration, indent);
            return new UTF8Encoding().GetString(payload);
        }
    }
}
