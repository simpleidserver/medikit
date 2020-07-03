// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;

namespace Medikit.Api.Application.Patient.Queries.Results
{
    public class ContactInformationResult
    {
        public ContactInformationTypes Type { get; set; }
        public string Value { get; set; }
    }
}
