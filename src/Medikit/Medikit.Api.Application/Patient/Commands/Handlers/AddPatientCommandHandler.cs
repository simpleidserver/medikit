// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Patient.Commands.Handlers
{
    public class AddPatientCommandHandler : IAddPatientCommandHandler
    {
        private readonly ICommitAggregateHelper _commitAggregateHelper;

        public AddPatientCommandHandler(ICommitAggregateHelper commitAggregateHelper)
        {
            _commitAggregateHelper = commitAggregateHelper;
        }

        public async Task<string> Handle(AddPatientCommand command, CancellationToken token)
        {
            var patient = PatientAggregate.New(command.PrescriberId, command.Firstname, command.Lastname, command.NationalIdentityNumber);
            var streamName = patient.GetStreamName();
            await _commitAggregateHelper.Commit(patient, streamName, Constants.QueueNames.Patient);
            return patient.Id;
        }
    }
}
