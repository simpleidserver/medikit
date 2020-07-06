// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries.Results;

namespace Medikit.Api.Medicalfile.Application.Medicalfile.Queries
{
    public class GetMedicalfileQuery : IRequest<GetMedicalfileResult>
    {
        public string Id { get; set; }
    }
}
