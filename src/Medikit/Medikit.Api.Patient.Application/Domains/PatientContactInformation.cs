// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.Api.Patient.Application.Domains
{
    public class PatientContactInformation : ICloneable
    {
        public ContactInformationTypes Type { get; set; }
        public string Value { get; set; }

        public object Clone()
        {
            return new PatientContactInformation
            {
                Type = Type,
                Value = Value
            };
        }
    }
}
