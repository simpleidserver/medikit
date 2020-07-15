// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.EHealth.Application.Resources;
using Medikit.EHealth.Exceptions;
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.Services.EHealthBox;
using Medikit.EHealth.Services.EHealthBox.Request;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.EHealth.Application.Message.Commands.Handlers
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, bool>
    {
        private readonly IEHealthBoxService _ehealthBoxService;

        public DeleteMessageCommandHandler(IEHealthBoxService eHealthBoxService)
        {
            _ehealthBoxService = eHealthBoxService;
        }

        public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            SAMLAssertion assertion;
            try
            {
                assertion = SAMLAssertion.Deserialize(request.AssertionToken);
            }
            catch
            {
                throw new BadAssertionTokenException(Global.BadAssertionToken);
            }

            await _ehealthBoxService.DeleteMessage(new EHealthBoxDeleteMessageRequest
            {
                MessageIdLst = request.MessageIds.ToList(),
                Source = request.Source
            }, assertion);
            return true;
        }
    }
}
