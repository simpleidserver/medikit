// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.Api.Application.Domains
{
    public class PatientAddress : ICloneable
    {
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public int Box { get; set; }

        public object Clone()
        {
            return new PatientAddress
            {
                Country = Country,
                PostalCode = PostalCode,
                Box = Box,
                Street = Street,
                StreetNumber = StreetNumber
            };
        }
    }
}
