// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Domains;
using System;
using System.Collections.Generic;

namespace Medikit.Api.Patient.Application.Domains.Events
{
    public class PatientAddedEvent : DomainEvent
    {
        public PatientAddedEvent(string id, string aggregateId, int version, string firstname, string lastname, string nationalIdentityNumber, DateTime createDateTime, DateTime updateDateTime, GenderTypes gender, DateTime birthDate, string logoUrl, string eidCardNumber, DateTime? eidCardValidity, PatientAddress address, ICollection<PatientContactInformation> contactInformations) : base(id, aggregateId, version)
        {
            Firstname = firstname;
            Lastname = lastname;
            NationalIdentityNumber = nationalIdentityNumber;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
            Gender = gender;
            BirthDate = birthDate;
            LogoUrl = logoUrl;
            EidCardNumber = eidCardNumber;
            EidCardValidity = eidCardValidity;
            Address = address;
            ContactInformations = contactInformations;
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public GenderTypes Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string LogoUrl { get; set; }
        public string EidCardNumber { get; set; }
        public DateTime? EidCardValidity { get; set; }
        public PatientAddress Address { get; set; }
        public ICollection<PatientContactInformation> ContactInformations { get; set; }
    }
}
