// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Infrastructure;
using System;

namespace Medikit.Api.Application.Domains.Events
{
    public class PatientAddedEvent : DomainEvent
    {
        public PatientAddedEvent(string id, string aggregateId, int version, string prescriberId, string firstname, string lastname, string nationalIdentityNumber, DateTime createDateTime) : base(id, aggregateId, version)
        {
            PrescriberId = prescriberId;
            Firstname = firstname;
            Lastname = lastname;
            NationalIdentityNumber = nationalIdentityNumber;
            CreateDateTime = createDateTime;
        }

        public string PrescriberId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
