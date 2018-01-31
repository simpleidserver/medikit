// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Domains.Events;
using Medikit.Api.Application.Infrastructure;
using Medikit.Api.Application.Infrastructure.Bus;
using Medikit.Api.Application.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Patient
{
    public class PatientEventHandler : IMessageBrokerConsumerGeneric<PatientAddedEvent>
    {
        private readonly IPatientCommandRepository _patientCommandRepository;

        public PatientEventHandler(IPatientCommandRepository patientCommandRepository)
        {
            _patientCommandRepository = patientCommandRepository;
        }

        public string QueueName => Constants.QueueNames.Patient;

        public async Task Handle(PatientAddedEvent message, CancellationToken token)
        {
            var patient = PatientAggregate.New(new List<DomainEvent>
            {
                message
            });
            await _patientCommandRepository.Add(patient, token);
            await _patientCommandRepository.Commit(token);
        }
    }
}
