// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Medikit.Api.Application.Prescriptions.Queries
{
    public class GetOpenedPharmaceuticalPrescriptionQuery
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
