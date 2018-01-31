// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Medikit.Api.Application.Prescriptions.Results
{
    public class GetPharmaceuticalPrescriptionPosology
    {
        public GetPharmaceuticalPrescriptionPosology(string type)
        {
            Type = type;
        }

        public string Type { get; set; }
    }
}
