// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.Services.Recipe.Request;

namespace Medikit.EHealth.EHealthServices.Parameters
{
    public class GetPrescriptionsParameter
    {
        public GetPrescriptionsParameter()
        {
            Page = new Page
            {
                PageNumber = 0
            };
        }

        public string PatientNiss { get; set; }
        public SAMLAssertion Assertion { get; set; }
        public Page Page { get; set; }
    }
}
