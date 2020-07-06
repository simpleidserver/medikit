// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.Api.Medicalfile.Application.Medicalfile.Queries.Results
{
    public class GetMedicalfileResult
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string PatientNiss { get; set; }
        public string PatientFirstname { get; set; }
        public string PatientLastname { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
