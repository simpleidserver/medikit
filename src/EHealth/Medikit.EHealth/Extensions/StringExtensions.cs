// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.IO;
using System.Linq;
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
            T samlEnv;
            using (var reader = new StringReader(xml))
            {
                samlEnv = (T)serializer.Deserialize(reader);
            }

            return samlEnv;
        }
    }
}
