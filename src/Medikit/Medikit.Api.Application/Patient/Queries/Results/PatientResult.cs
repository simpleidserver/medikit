// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.Api.Application.Patient.Queries.Results
{
    public class PatientResult
    {
        public string Niss { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string LogoUrl { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
