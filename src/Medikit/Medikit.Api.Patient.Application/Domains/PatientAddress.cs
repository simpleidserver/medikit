// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;

namespace Medikit.Api.Patient.Application.Domains
{
    public class PatientAddress : ICloneable
    {
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public IEnumerable<double> Coordinates { get; set; }

        public object Clone()
        {
            return new PatientAddress
            {
                Country = Country,
                PostalCode = PostalCode,
                Street = Street,
                StreetNumber = StreetNumber,
                Coordinates = Coordinates
            };
        }
    }
}
