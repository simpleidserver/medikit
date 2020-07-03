// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Medikit.Api.Application.Patient.Queries
{
    public class GetPatientByIdQuery
    {
        public GetPatientByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
