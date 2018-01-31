// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Medikit.Api.Application.Extensions
{
    public static class ConcurrentBagExtensions
    {
        public static void Remove<T>(this ConcurrentBag<T> bag, T item)
        {
            var lst = new List<T>();
            while (bag.Count > 0)
            {
                T result;
                bag.TryTake(out result);
                if (result.Equals(item))
                {
                    break;
                }

                lst.Add(result);
            }

            foreach (var l in lst)
            {
                bag.Add(l);
            }
        }
    }
}
