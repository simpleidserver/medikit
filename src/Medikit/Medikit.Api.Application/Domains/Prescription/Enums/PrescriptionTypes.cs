// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Medikit.Api.Application.Domains
{
    public enum PrescriptionTypes
    {
        P0 = 0, // Pharmaceutical prescription
        P1 = 1, // Pharmaceutical prescription that necessitates information on the patient’s insurability
        P2 = 2  // Pharmaceutical prescription that necessitates attestation information
    }
}