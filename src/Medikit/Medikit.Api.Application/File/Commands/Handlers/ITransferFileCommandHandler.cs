// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;

namespace Medikit.Api.Application.File.Commands.Handlers
{
    public interface ITransferFileCommandHandler
    {
        Task<string> Handle(TransferFileCommand command);
    }
}
