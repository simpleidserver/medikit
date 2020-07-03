// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace Medikit.Api.Application.Patient.Queries.Results
{
    public class AddressResult
    {
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public IEnumerable<double> Coordinates { get; set; }
    }
}
