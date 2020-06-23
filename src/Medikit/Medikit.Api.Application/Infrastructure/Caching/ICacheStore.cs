// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Infrastructure.Caching
{
    public interface ICacheStore
    {
        Task<bool> Add<T>(string key, T value, CancellationToken token) where T : class;
        Task<T> Get<T>(string key, CancellationToken token) where T : class;
        Task<bool> Remove(string key);
    }
}
