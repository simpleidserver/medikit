// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;

namespace Medikit.Api.QRFile.Application.Commands
{
    public class TransferQRFileCommand : IRequest<string>
    {
        public string File { get; set; }
    }
}
