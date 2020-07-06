// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.QRFile.Application.Commands;
using Medikit.Api.QRFile.Application.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.QRFile.Application
{
    public class QRFileService : IQRFileService
    {
        private readonly IMediator _mediator;

        public QRFileService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<string> Transfer(TransferQRFileCommand command, CancellationToken token)
        {
            return _mediator.Send(command, token);
        }

        public Task<string> Get(string file, CancellationToken token)
        {
            return _mediator.Send(new GetQRFileQuery { FileId = file }, token);
        }
    }
}
