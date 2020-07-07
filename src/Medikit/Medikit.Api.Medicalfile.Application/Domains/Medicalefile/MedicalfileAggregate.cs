// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Domains;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

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
            var id = BuildId(prescriberId, patientId);
            var evt = new MedicalfileAddedEvent(Guid.NewGuid().ToString(), id, 0, prescriberId, patientId, patientNiss, patientFirstname, patientLastname, DateTime.UtcNow, DateTime.UtcNow);
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

        public static string BuildId(string prescriberId, string patientId)
        {
            using (var sha256Hash = SHA256.Create())
            {
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes($"{prescriberId}{patientId}"));
                var builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
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
