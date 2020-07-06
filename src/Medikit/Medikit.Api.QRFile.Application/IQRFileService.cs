// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.QRFile.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.QRFile.Application
{
    public interface IQRFileService
    {
        Task<string> Transfer(TransferQRFileCommand command, CancellationToken token);
        Task<string> Get(string file, CancellationToken token);
    }
}
