// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Bus;
using Medikit.Api.Common.Application.Domains;
using Medikit.Api.Medicalfile.Application.Domains;
using Medikit.Api.Medicalfile.Application.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Medicalfile
{
    public class MedicalfileEventHandler : IMessageBrokerConsumerGeneric<MedicalfileAddedEvent>
    {
        private readonly IMedicalfileCommandRepository _medicalfileCommandRepository;

        public MedicalfileEventHandler(IMedicalfileCommandRepository medicalfileCommandRepository)
        {
            _medicalfileCommandRepository = medicalfileCommandRepository;
        }

        public string QueueName => Constants.QueueNames.Medicalfile;

        public async Task Handle(MedicalfileAddedEvent message, CancellationToken token)
        {
            var medicalfile = MedicalfileAggregate.New(new List<DomainEvent>
            {
                message
            });
            await _medicalfileCommandRepository.Add(medicalfile, token);
            await _medicalfileCommandRepository.Commit(token);
        }
    }
}
