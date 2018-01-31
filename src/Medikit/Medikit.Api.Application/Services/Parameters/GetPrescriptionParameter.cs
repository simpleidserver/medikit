// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SAML.DTOs;

namespace Medikit.Api.Application.Services.Parameters
{
    public class GetPrescriptionParameter
    {
        public string PrescriptionId { get; set; }
        public SAMLAssertion Assertion { get; set; }
    }
}
