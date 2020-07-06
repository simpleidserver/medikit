// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Patient.Application.Domains;
using System;
using System.Collections.Generic;

namespace Medikit.Api.Patient.Application.Queries.Results
{
    public class GetPatientQueryResult
    {
        public GetPatientQueryResult()
        {
            ContactInformations = new List<ContactInformationResult>();
        }

        public string Id { get; set; }
        public string Niss { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EidCardNumber { get; set; }
        public DateTime? EidCardValidity { get; set; }
        public string LogoUrl { get; set; }
        public GenderTypes Gender { get; set; }
        public ICollection<ContactInformationResult> ContactInformations { get; set; }
        public AddressResult Address { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public class ContactInformationResult
        {
            public ContactInformationTypes Type { get; set; }
            public string Value { get; set; }
        }

        public class AddressResult
        {
            public string Country { get; set; }
            public string PostalCode { get; set; }
            public string Street { get; set; }
            public int StreetNumber { get; set; }
            public IEnumerable<double> Coordinates { get; set; }
        }
    }
}
