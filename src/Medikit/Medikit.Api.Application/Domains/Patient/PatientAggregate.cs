// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains.Events;
using Medikit.Api.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Medikit.Api.Application.Domains
{
    public class PatientAggregate : BaseAggregate
    {
        public PatientAggregate()
        {
            Addresses = new List<PatientAddress>();
            ContactInformations = new List<PatientContactInformation>();
        }

        public string PrescriberId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public GenderTypes Gender { get; set; }
        public string NationalIdentityNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string LogoUrl { get; set; }
        public string EidCardNumber { get; set; }
        public DateTime? EidCardValidity { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public ICollection<PatientAddress> Addresses { get; set; }
        public ICollection<PatientContactInformation> ContactInformations { get; set; }

        public static PatientAggregate New(string id, string prescriberId, string firstName, string lastName, string nationalIdentityNumber, GenderTypes gender, DateTime birthDate, string logoUrl, string eidCardNumber, DateTime? eidCardValidity, ICollection<PatientAddress> addresses, ICollection<PatientContactInformation> contactInformations)
        {
            var evt = new PatientAddedEvent(Guid.NewGuid().ToString(), id, 0, prescriberId, firstName, lastName, nationalIdentityNumber, DateTime.UtcNow, gender, birthDate, logoUrl, eidCardNumber, eidCardValidity, addresses, contactInformations);
            var result = new PatientAggregate();
            result.Handle(evt);
            result.DomainEvents.Add(evt);
            return result;
        }

        public static PatientAggregate New(ICollection<DomainEvent> evts)
        {
            var result = new PatientAggregate();
            foreach(var evt in evts)
            {
                result.Handle(evt);
            }

            return result;
        }

        public override object Clone()
        {
            return new PatientAggregate
            {
                PrescriberId = PrescriberId,
                CreateDateTime = CreateDateTime,
                Firstname = Firstname,
                Lastname = Lastname,
                NationalIdentityNumber = NationalIdentityNumber,
                Version = Version,
                Id = Id,
                LogoUrl = LogoUrl,
                BirthDate = BirthDate,
                UpdateDateTime = UpdateDateTime,
                Addresses = Addresses.Select(_ => (PatientAddress)_.Clone()).ToList(),
                ContactInformations = ContactInformations.Select(_ => (PatientContactInformation)_.Clone()).ToList(),
                EidCardNumber = EidCardNumber,
                EidCardValidity = EidCardValidity,
                Gender = Gender
            };
        }

        public override void Handle(dynamic obj)
        {
            Handle(obj);
        }

        public void Handle(PatientAddedEvent evt)
        {
            Id = evt.AggregateId;
            PrescriberId = evt.PrescriberId;
            CreateDateTime = evt.CreateDateTime;
            Firstname = evt.Firstname;
            Lastname = evt.Lastname;
            Gender = evt.Gender;
            NationalIdentityNumber = evt.NationalIdentityNumber;
            BirthDate = evt.BirthDate;
            LogoUrl = evt.LogoUrl;
            EidCardNumber = evt.EidCardNumber;
            EidCardValidity = evt.EidCardValidity;
            Addresses = evt.Addresses;
            ContactInformations = evt.ContactInformations;
        }

        public string GetStreamName()
        {
            return GetStreamName(Id);
        }

        public static string GetStreamName(string id)
        {
            return $"patients-{id}";
        }
    }
}
