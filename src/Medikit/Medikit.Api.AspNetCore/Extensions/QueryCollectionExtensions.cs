// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Medikit.Api.AspNetCore.Extensions
{
    public static class QueryCollectionExtensions
    {
        public static IEnumerable<KeyValuePair<string, object>> ToEnumerable(this IQueryCollection query)
        {
            var result = new List<KeyValuePair<string, object>>();
            foreach (var record in query)
            {
                result.Add(new KeyValuePair<string, object>(record.Key, record.Value));
            }

            return result;
        }

        public static bool TryGet(this IEnumerable<KeyValuePair<string, object>> queryCollection, string name, out string[] values)
        {
            values = null;
            if (queryCollection.ContainsKey(name))
            {
                var result = queryCollection.Get(name).ToArray().Where(s => s != null).Distinct();
                if (!result.Any())
                {
                    return false;
                }

                values = result.Select(_ => _.ToString()).ToArray();
                return true;
            }

            return false;
        }

        public static bool TryGet<T>(this IEnumerable<KeyValuePair<string, object>> queryCollection, string name, out T enumeration) where T : struct
        {
            string val;
            enumeration = default(T);
            if (!queryCollection.TryGet(name, out val))
            {
                return false;
            }

            T result;
            if (Enum.TryParse<T>(val, true, out result))
            {
                enumeration = result;
                return true;
            }

            return false;
        }

        public static bool TryGet(this IEnumerable<KeyValuePair<string, object>> queryCollection, string name, out int[] values)
        {
            values = null;
            string[] tmp;
            if (!queryCollection.TryGet(name, out tmp))
            {
                return false;
            }

            var result = new List<int>();
            foreach (var str in tmp)
            {
                var splitted = str.Split(',');
                foreach (var record in splitted)
                {
                    int number;
                    if (int.TryParse(record, out number))
                    {
                        result.Add(number);
                    }
                }
            }

            if (!result.Any())
            {
                return false;
            }

            values = result.ToArray();
            return true;
        }

        public static bool TryGet(this IEnumerable<KeyValuePair<string, object>> queryCollection, string name, out DateTime value)
        {
            value = default(DateTime);
            if (queryCollection.ContainsKey(name))
            {
                DateTime result;
                if (DateTime.TryParse(queryCollection.Get(name).ToArray().First().ToString(), out result))
                {
                    value = result;
                    return true;
                }

                return false;
            }

            return false;
        }

        public static bool TryGet(this IEnumerable<KeyValuePair<string, object>> queryCollection, string name, out bool value)
        {
            value = false;
            if (queryCollection.ContainsKey(name))
            {
                bool result;
                if (bool.TryParse(queryCollection.Get(name).ToArray().First().ToString(), out result))
                {
                    value = result;
                    return true;
                }

                return false;
            }

            return false;
        }

        public static bool TryGet(this IEnumerable<KeyValuePair<string, object>> queryCollection, string name, out string value)
        {
            value = null;
            if (queryCollection.ContainsKey(name))
            {
                value = queryCollection.Get(name).ToArray().First().ToString();
                return true;
            }

            return false;
        }

        public static bool TryGet(this IEnumerable<KeyValuePair<string, object>> queryCollection, string name, out IEnumerable<object> values)
        {
            values = null;
            if (queryCollection.ContainsKey(name))
            {
                values = queryCollection.Get(name);
                return true;
            }

            return false;
        }

        public static bool TryGet(this IEnumerable<KeyValuePair<string, object>> queryCollection, string name, out int startIndex)
        {
            startIndex = 0;
            if (queryCollection.ContainsKey(name))
            {
                return int.TryParse(queryCollection.Get(name).First().ToString(), out startIndex);
            }

            return false;
        }

        public static bool ContainsKey(this IEnumerable<KeyValuePair<string, object>> queryCollection, string name)
        {
            if (queryCollection.Any(q => q.Key == name))
            {
                return true;
            }

            return false;
        }

        public static IEnumerable<object> Get(this IEnumerable<KeyValuePair<string, object>> queryCollection, string name)
        {
            if (!queryCollection.ContainsKey(name))
            {
                return new string[0];
            }

            return queryCollection.Where(q => q.Key == name).Select(q => q.Value);
        }
    }
}
