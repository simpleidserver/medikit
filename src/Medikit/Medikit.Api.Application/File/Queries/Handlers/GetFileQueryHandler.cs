// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Exceptions;
using Medikit.Api.Application.Infrastructure.Caching;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.File.Queries.Handlers
{
    public class GetFileQueryHandler : IGetFileQueryHandler
    {
        private readonly ICacheStore _store;

        public GetFileQueryHandler(ICacheStore store)
        {
            _store = store;
        }

        public async Task<string> Handle(string fileId)
        {
            var cacheKey = $"file-{fileId}";
            var result = await _store.Get<string>(cacheKey, CancellationToken.None);
            if (result == null)
            {
                throw new UnknownFileException(fileId);
            }

            return result;
        }
    }
}
