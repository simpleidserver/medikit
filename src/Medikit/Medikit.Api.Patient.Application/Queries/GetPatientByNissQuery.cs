// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Patient.Application.Queries.Results;

namespace Medikit.Api.Patient.Application.Queries
{
    public class GetPatientByNissQuery : IRequest<GetPatientQueryResult>
    {
        public GetPatientByNissQuery(string niss)
        {
            Niss = niss;
        }

        public string Niss { get; set; }
    }
}
