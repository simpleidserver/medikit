// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application;
using Medikit.Api.Common.Application.EvtStore;
using Medikit.Api.Common.Application.Exceptions;
using Medikit.Api.Medicalfile.Application.Domains;
using Medikit.Api.Medicalfile.Application.Extensions;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries.Results;
using Medikit.Api.Medicalfile.Application.Resources;
using Medikit.Api.Patient.Application.Exceptions;
using Medikit.Api.Patient.Application.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Medicalfile.Commands.Handlers
{
    public class AddMedicalfileCommandHandler : IRequestHandler<AddMedicalfileCommand, GetMedicalfileResult>
    {
        private readonly IPatientQueryRepository _patientQueryRepository;
        private readonly ICommitAggregateHelper _commitAggregateHelper;
        private readonly IEventStoreRepository _eventStoreRepository;

        public AddMedicalfileCommandHandler(IPatientQueryRepository patientQueryRepository, ICommitAggregateHelper commitAggregateHelper, IEventStoreRepository eventStoreRepository)
        {
            _patientQueryRepository = patientQueryRepository;
            _commitAggregateHelper = commitAggregateHelper;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<GetMedicalfileResult> Handle(AddMedicalfileCommand request, CancellationToken cancellationToken)
        {
            var id = MedicalfileAggregate.BuildId(request.PrescriberId, request.PatientId);
            var medicalfile = await _eventStoreRepository.GetLastAggregate<MedicalfileAggregate>(id, MedicalfileAggregate.GetStreamName(id));
            if (medicalfile != null && !string.IsNullOrWhiteSpace(medicalfile.Id))
            {
                throw new BadRequestException(Global.ConcurrentMedicalfile);
            }

            var patient = await _patientQueryRepository.GetById(request.PatientId, cancellationToken);
            if (patient == null)
            {
                throw new UnknownPatientException(request.PatientId, string.Format(Global.UnknownPatient, request.PatientId));
            }

            var medicalFile = MedicalfileAggregate.New(request.PrescriberId, request.PatientId, patient.NationalIdentityNumber, patient.Firstname, patient.Lastname);
            await _commitAggregateHelper.Commit(medicalFile, medicalFile.GetStreamName(), Constants.QueueNames.Medicalfile);
            return medicalFile.ToResult();
        }
    }
}
