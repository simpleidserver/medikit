// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Infrastructure.Caching;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.File.Commands.Handlers
{
    public class TransferFileCommandHandler : ITransferFileCommandHandler
    {
        private readonly ICacheStore _store;

        public TransferFileCommandHandler(ICacheStore store)
        {
            _store = store;
        }

        public async Task<string> Handle(TransferFileCommand command)
        {
            var id = Guid.NewGuid().ToString();
            var cacheKey = $"file-{id}";
            await _store.Add(cacheKey, command.File, CancellationToken.None);
            return id;
        }
    }
}
