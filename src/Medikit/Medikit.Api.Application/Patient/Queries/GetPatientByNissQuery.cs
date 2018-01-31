// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Medikit.Api.Application.Patient.Queries
{
    public class GetPatientByNissQuery
    {
        public GetPatientByNissQuery(string niss)
        {
            Niss = niss;
        }

        public string Niss { get; set; }
    }
}
