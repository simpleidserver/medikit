// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;

namespace Medikit.Api.Medicalfile.Application.Prescription.Commands
{
    public class RevokePrescriptionCommand : IRequest<bool>
    {
        public string AssertionToken { get; set; }
        public string Rid { get; set; }
        public string Reason { get; set; }
    }
}
