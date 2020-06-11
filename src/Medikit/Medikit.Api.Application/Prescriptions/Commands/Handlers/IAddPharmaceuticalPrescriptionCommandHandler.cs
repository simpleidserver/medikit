// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Prescriptions.Commands.Handlers
{
    public interface IAddPharmaceuticalPrescriptionCommandHandler
    {
        Task<string> Handle(AddPharmaceuticalPrescriptionCommand command, CancellationToken token);
    }
}
