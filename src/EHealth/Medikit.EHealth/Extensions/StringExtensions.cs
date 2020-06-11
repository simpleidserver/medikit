// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Medikit.EHealth.Extensions
{
    public static class StringExtensions
    {
        public static string ClearBadFormat(this string str)
        {
            return new string(str.Where(c => !char.IsControl(c)).ToArray());
        }

        public static T Deserialize<T>(this string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            var byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (xml.StartsWith(byteOrderMarkUtf8))
            {
                xml = xml.Remove(0, byteOrderMarkUtf8.Length);
            }

            T result;
            using (var reader = new StringReader(xml))
            {
                result = (T)serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
