// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Medikit.Api.Application.Prescriptions.Queries
{
    public class GetPharmaceuticalPrescriptionQuery
    {
        public string PrescriptionId { get; set; }
        public string AssertionToken { get; set; }
    }
}
