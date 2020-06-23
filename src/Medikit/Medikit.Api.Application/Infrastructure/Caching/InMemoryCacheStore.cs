// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Infrastructure.Caching
{
    public class InMemoryCacheStore : ICacheStore
    {
        private ConcurrentDictionary<string, string> _dic;

        public InMemoryCacheStore()
        {
            _dic = new ConcurrentDictionary<string, string>();
        }

        public Task<bool> Add<T>(string key, T value, CancellationToken token) where T : class
        {
            _dic.TryAdd(key, JsonConvert.SerializeObject(value));
            return Task.FromResult(true);
        }

        public Task<T> Get<T>(string key, CancellationToken token) where T : class
        {
            if (!_dic.ContainsKey(key))
            {
                return Task.FromResult((T)null);
            }


            var val = _dic[key];
            return Task.FromResult(JsonConvert.DeserializeObject<T>(val));
        }

        public Task<bool> Remove(string key)
        {
            string val;
            if (!_dic.TryRemove(key, out val))
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
