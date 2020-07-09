// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Medicalfile.Application.Prescription.Results;

namespace Medikit.Api.Medicalfile.Application.Prescription.Queries
{
    public class GetOpenedPharmaceuticalPrescriptionsQuery : IRequest<SearchPharmaceuticalPrescriptionResult>
    {
        public GetOpenedPharmaceuticalPrescriptionsQuery()
        {
            PageNumber = 0;
        }

        public string MedicalfileId { get; set; }
        public string AssertionToken { get; set; }
        public int PageNumber { get; set; }
    }
}
