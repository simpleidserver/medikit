// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.EID.Tlv
{
    public class TlvIdentity
    {
        [TlvField(1)]
        public string CardNumber { get; set; }
        [TlvField(2)]
        public string ChipNumber { get; set; }
        [TlvField(5)]
        public string CardDeliveryMunicipality { get; set; }
        [TlvField(6)]
        public string NationalNumber { get; set; }
        [TlvField(7)]
        public string Name { get; set; }
        [TlvField(8)]
        public string FirstName { get; set; }
        [TlvField(9)]
        public string MiddleName { get; set; }
        [TlvField(10)]
        public string Nationality { get; set; }
        [TlvField(11)]
        public string PlaceOfBirth { get; set; }
        [TlvField(13)]
        public string Gender { get; set; }
        [TlvField(14)]
        public string NobleCondition { get; set; }
        [TlvField(15)]
        public DocumentTypes DocumentType { get; set; }
        [TlvField(16)]
        public SpecialStatus SpecialStatus { get; set; }
        [TlvField(17)]
        public byte[] PhotoDigest { get; set; }
        [TlvField(18)]
        public string Duplicate { get; set; }
        [TlvField(19)]
        public SpecialOrganisations? SpecialOrganisation { get; set; }
        [TlvField(20)]
        public bool MemberOfFamily { get; set; }
        [TlvField(21)]
        public string DateAndCountryOfProtection { get; set; }
        [TlvField(22)]
        public DateTime DateOfProtection { get; set; }
        [TlvField(23)]
        public string CountryOfProtection { get; set; }
    }
}
