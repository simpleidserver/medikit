// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.File.Commands;
using Medikit.Api.Application.File.Commands.Handlers;
using Medikit.Api.Application.File.Queries.Handlers;
using System.Threading.Tasks;

namespace Medikit.Api.Application.File
{
    public class FileService : IFileService
    {
        private readonly ITransferFileCommandHandler _transferFileCommandHandler;
        private readonly IGetFileQueryHandler _getFileQueryHandler;

        public FileService(ITransferFileCommandHandler transferFileCommandHandler, IGetFileQueryHandler getFileQueryHandler)
        {
            _transferFileCommandHandler = transferFileCommandHandler;
            _getFileQueryHandler = getFileQueryHandler;
        }

        public Task<string> Transfer(TransferFileCommand command)
        {
            return _transferFileCommandHandler.Handle(command);
        }

        public Task<string> Get(string file)
        {
            return _getFileQueryHandler.Handle(file);
        }
    }
}
