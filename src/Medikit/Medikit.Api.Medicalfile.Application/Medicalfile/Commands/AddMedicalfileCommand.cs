// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries.Results;

namespace Medikit.Api.Medicalfile.Application.Medicalfile.Commands
{
    public class AddMedicalfileCommand : IRequest<GetMedicalfileResult>
    {
        public string PrescriberId { get; set; }
        public string PatientId { get; set; }
    }
}
