﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using System;
using System.Collections.Generic;

namespace Medikit.Api.Application.Patient.Commands
{
    public class AddPatientCommand
    {
        public string PrescriberId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public GenderTypes Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string LogoUrl { get; set; }
        public string EidCardNumber { get; set; }
        public DateTime? EidCardValidity { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<ContactInformation> ContactInformations { get; set; }

        public class Address
        {
            public string Country { get; set; }
            public string PostalCode { get; set; }
            public string Street { get; set; }
            public int StreetNumber { get; set; }
            public int Box { get; set; }
        }

        public class ContactInformation
        {
            public ContactInformationTypes Type { get; set; }
            public string Value { get; set; }
        }
    }
}
