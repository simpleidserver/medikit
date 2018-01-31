// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Medikit.Api.Application.Patient.Commands
{
    public class AddPatientCommand
    {
        public string PrescriberId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string NationalIdentityNumber { get; set; }
    }
}
