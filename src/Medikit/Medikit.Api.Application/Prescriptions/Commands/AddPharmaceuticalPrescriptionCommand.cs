// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;

namespace Medikit.Api.Application.Prescriptions.Commands
{
    public class AddPharmaceuticalPrescriptionCommand
    {
        public PharmaceuticalPrescription Prescription { get; set; }
    }
}
