// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.File.Commands;
using System.Threading.Tasks;

namespace Medikit.Api.Application.File
{
    public interface IFileService
    {
        Task<string> Transfer(TransferFileCommand command);
        Task<string> Get(string file);
    }
}
