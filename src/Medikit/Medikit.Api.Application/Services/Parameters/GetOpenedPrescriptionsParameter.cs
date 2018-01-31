// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SAML.DTOs;

namespace Medikit.Api.Application.Services.Parameters
{
    public class GetOpenedPrescriptionsParameter
    {
        public GetOpenedPrescriptionsParameter()
        {
            PageNumber = 0;
        }

        public string PatientNiss { get; set; }
        public SAMLAssertion Assertion { get; set; }
        public int PageNumber { get; set; }
    }
}
