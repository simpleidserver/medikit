// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application.Caching;
using Medikit.Api.QRFile.Application.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.QRFile.Application.Queries.Handlers
{
    public class GetQRFileQueryHandler : IRequestHandler<GetQRFileQuery, string>
    {
        private readonly ICacheStore _store;

        public GetQRFileQueryHandler(ICacheStore store)
        {
            _store = store;
        }

        public async Task<string> Handle(GetQRFileQuery query, CancellationToken token)
        {
            var cacheKey = $"file-{query.FileId}";
            var result = await _store.Get<string>(cacheKey, token);
            if (result == null)
            {
                throw new UnknownQRFileException(query.FileId);
            }

            return result;
        }
    }
}
