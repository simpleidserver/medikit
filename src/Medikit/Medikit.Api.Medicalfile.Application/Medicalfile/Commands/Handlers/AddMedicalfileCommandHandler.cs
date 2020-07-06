// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application;
using Medikit.Api.Medicalfile.Application.Domains;
using Medikit.Api.Medicalfile.Application.Resources;
using Medikit.Api.Patient.Application.Exceptions;
using Medikit.Api.Patient.Application.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Medicalfile.Commands.Handlers
{
    public class AddMedicalfileCommandHandler : IRequestHandler<AddMedicalfileCommand, string>
    {
        private readonly IPatientQueryRepository _patientQueryRepository;
        private readonly ICommitAggregateHelper _commitAggregateHelper;

        public AddMedicalfileCommandHandler(IPatientQueryRepository patientQueryRepository, ICommitAggregateHelper commitAggregateHelper)
        {
            _patientQueryRepository = patientQueryRepository;
            _commitAggregateHelper = commitAggregateHelper;
        }

        public async Task<string> Handle(AddMedicalfileCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientQueryRepository.GetById(request.PatientId, cancellationToken);
            if (patient == null)
            {
                throw new UnknownPatientException(request.PatientId, string.Format(Global.UnknownPatient, request.PatientId));
            }

            var medicalFile = MedicalfileAggregate.New(request.PrescriberId, request.PatientId, patient.NationalIdentityNumber, patient.Firstname, patient.Lastname);
            await _commitAggregateHelper.Commit(medicalFile, medicalFile.GetStreamName(), Constants.QueueNames.Medicalfile);
            return medicalFile.Id;
        }
    }
}
