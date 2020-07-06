// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using System.Collections.Generic;

namespace Medikit.Api.Medicalfile.Application.Prescription.Queries
{
    public class GetOpenedPharmaceuticalPrescriptionQuery : IRequest<ICollection<string>>
    {
        public GetOpenedPharmaceuticalPrescriptionQuery()
        {
            PageNumber = 0;
        }

        public string PatientNiss { get; set; }
        public string AssertionToken { get; set; }
        public int PageNumber { get; set; }
    }
}
