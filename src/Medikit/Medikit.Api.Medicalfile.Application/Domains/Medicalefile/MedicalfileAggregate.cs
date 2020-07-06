// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Domains;
using System;
using System.Collections.Generic;

namespace Medikit.Api.Medicalfile.Application.Domains
{
    public class MedicalfileAggregate : BaseAggregate
    {
        public MedicalfileAggregate()
        {

        }

        public string PrescriberId { get; set; }
        public string PatientId { get; set; }
        public string PatientNiss { get; set; }
        public string PatientFirstname { get; set; }
        public string PatientLastname { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public static MedicalfileAggregate New(string prescriberId, string patientId, string patientNiss, string patientFirstname, string patientLastname)
        {
            var evt = new MedicalfileAddedEvent(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 0, prescriberId, patientId, patientNiss, patientFirstname, patientLastname, DateTime.UtcNow, DateTime.UtcNow);
            var result = new MedicalfileAggregate();
            result.Handle(evt);
            result.DomainEvents.Add(evt);
            return result;
        }

        public override object Clone()
        {
            return new MedicalfileAggregate
            {
                Id = Id,
                Version = Version,
                PrescriberId = PrescriberId,
                PatientId = PatientId,
                CreateDateTime = CreateDateTime,
                UpdateDateTime = UpdateDateTime,
                PatientFirstname = PatientFirstname,
                PatientLastname = PatientLastname,
                PatientNiss = PatientNiss
            };
        }

        public string GetStreamName()
        {
            return GetStreamName(Id);
        }

        public static string GetStreamName(string id)
        {
            return $"medicalfiles-{id}";
        }

        public override void Handle(dynamic obj)
        {
            Handle(obj);
        }

        public static MedicalfileAggregate New(IEnumerable<DomainEvent> events)
        {
            var result = new MedicalfileAggregate();
            foreach(var evt in events)
            {
                result.Handle(evt);
            }

            return result;
        }

        private void Handle(MedicalfileAddedEvent evt)
        {
            Id = evt.AggregateId;
            Version = evt.Version;
            PrescriberId = evt.PrescriberId;
            PatientId = evt.PatientId;
            PatientNiss = evt.PatientNiss;
            PatientFirstname = evt.PatientFirstname;
            PatientLastname = evt.PatientLastname;
            CreateDateTime = evt.CreateDateTime;
            UpdateDateTime = evt.UpdateDateTime;
        }
    }
}
