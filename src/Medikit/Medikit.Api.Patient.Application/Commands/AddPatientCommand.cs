// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Patient.Application.Domains;
using System;
using System.Collections.Generic;

namespace Medikit.Api.Patient.Application.Commands
{
    public class AddPatientCommand : IRequest<string>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public GenderTypes Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Base64EncodedImage { get; set; }
        public string EidCardNumber { get; set; }
        public DateTime? EidCardValidity { get; set; }
        public Address PatientAddress { get; set; }
        public ICollection<ContactInformation> ContactInformations { get; set; }

        public class Address
        {
            public string Country { get; set; }
            public string PostalCode { get; set; }
            public string Street { get; set; }
            public int StreetNumber { get; set; }
            public IEnumerable<double> Coordinates { get; set; }
        }

        public class ContactInformation
        {
            public ContactInformationTypes Type { get; set; }
            public string Value { get; set; }
        }
    }
}
