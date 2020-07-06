// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application.Caching;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.QRFile.Application.Commands.Handlers
{
    public class TransferQRFileCommandHandler : IRequestHandler<TransferQRFileCommand, string>
    {
        private readonly ICacheStore _store;

        public TransferQRFileCommandHandler(ICacheStore store)
        {
            _store = store;
        }

        public async Task<string> Handle(TransferQRFileCommand command, CancellationToken token)
        {
            var id = Guid.NewGuid().ToString();
            var cacheKey = $"file-{id}";
            await _store.Add(cacheKey, command.File, token);
            return id;
        }
    }
}
