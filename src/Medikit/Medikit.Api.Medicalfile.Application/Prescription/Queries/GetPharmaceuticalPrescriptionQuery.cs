// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Medicalfile.Prescription.Prescription.Results;

namespace Medikit.Api.Medicalfile.Application.Prescription.Queries
{
    public class GetPharmaceuticalPrescriptionQuery : IRequest<GetPharmaceuticalPrescriptionResult>
    {
        public string PrescriptionId { get; set; }
        public string AssertionToken { get; set; }
    }
}
