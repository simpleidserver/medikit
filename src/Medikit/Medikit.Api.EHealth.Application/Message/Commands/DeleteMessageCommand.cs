// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.EHealth.Services.EHealthBox;
using System.Collections.Generic;

namespace Medikit.Api.EHealth.Application.Message.Commands
{
    public class DeleteMessageCommand : IRequest<bool>
    {
        public string AssertionToken { get; set; }
        public ICollection<string> MessageIds { get; set; }
        public EHealthBoxSources Source { get; set; }
    }
}
