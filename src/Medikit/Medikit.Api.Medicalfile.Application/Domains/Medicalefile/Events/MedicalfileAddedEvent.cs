// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Domains;
using System;

namespace Medikit.Api.Medicalfile.Application.Domains
{
    public class MedicalfileAddedEvent : DomainEvent
    {
        public MedicalfileAddedEvent(string id, string aggregateId, int version, string prescriberId, string patientId, string patientNiss, string patientFirstname, string patientLastname, DateTime createDateTime, DateTime updateDateTime) : base(id, aggregateId, version)
        {
            PrescriberId = prescriberId;
            PatientId = patientId;
            PatientNiss = patientNiss;
            PatientFirstname = patientFirstname;
            PatientLastname = patientLastname;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
        }

        public string PrescriberId { get; set; }
        public string PatientId { get; set; }
        public string PatientNiss { get; set; }
        public string PatientFirstname { get; set; }
        public string PatientLastname { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
