// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Medicalfile.Application.Resources;
using Medikit.EHealth.Exceptions;
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.Services.Recipe;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Prescription.Commands.Handlers
{
    public class RevokePrescriptionCommandHandler : IRequestHandler<RevokePrescriptionCommand, bool>
    {
        private readonly IRecipeService _recipeService;

        public RevokePrescriptionCommandHandler(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public async Task<bool> Handle(RevokePrescriptionCommand command, CancellationToken token)
        {
            SAMLAssertion assertion;
            try
            {
                assertion = SAMLAssertion.Deserialize(command.AssertionToken);
            }
            catch
            {
                throw new BadAssertionTokenException(Global.BadAssertionToken);
            }

            await _recipeService.RevokePrescription(command.Rid, command.Reason, assertion);
            return true;
        }
    }
}
