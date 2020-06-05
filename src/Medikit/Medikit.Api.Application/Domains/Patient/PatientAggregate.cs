// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains.Events;
using Medikit.Api.Application.Infrastructure;
using System;
using System.Collections.Generic;

namespace Medikit.Api.Application.Domains
{
    public class PatientAggregate : BaseAggregate
    {
        public string PrescriberId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public static PatientAggregate New(string prescriberId, string firstName, string lastName, string nationalIdentityNumber)
        {
            var evt = new PatientAddedEvent(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 0, prescriberId, firstName, lastName, nationalIdentityNumber, DateTime.UtcNow);
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
                Id = Id
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
            NationalIdentityNumber = evt.NationalIdentityNumber;
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
