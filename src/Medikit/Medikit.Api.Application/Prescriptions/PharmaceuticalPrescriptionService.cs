// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Metadata;
using Medikit.Api.Application.Prescriptions.Commands;
using Medikit.Api.Application.Prescriptions.Commands.Handlers;
using Medikit.Api.Application.Prescriptions.Queries;
using Medikit.Api.Application.Prescriptions.Queries.Handlers;
using Medikit.Api.Application.Prescriptions.Results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Prescriptions
{
    public class PharmaceuticalPrescriptionService : IPharmaceuticalPrescriptionService
    {
        private readonly IAddPharmaceuticalPrescriptionCommandHandler _addPharmaceuticalPrescriptionCommandHandler;
        private readonly IGetOpenedPharmaceuticalPrescriptionQueryHandler _getOpenedPharmaceuticalPrescriptionQueryHandler;
        private readonly IGetPharmaceuticalPrescriptionQueryHandler _getPharmaceuticalPrescriptionQueryHandler;
        private readonly IGetPrescriptionMetadataQueryHandler _getPrescriptionMetadataQueryHandler;
        private readonly IRevokePrescriptionCommandHandler _revokePrescriptionCommandHandler;

        public PharmaceuticalPrescriptionService(IAddPharmaceuticalPrescriptionCommandHandler addPharmaceuticalPrescriptionCommandHandler, 
            IGetOpenedPharmaceuticalPrescriptionQueryHandler getOpenedPharmaceuticalPrescriptionQueryHandler, 
            IGetPharmaceuticalPrescriptionQueryHandler getPharmaceuticalPrescriptionQueryHandler,
            IGetPrescriptionMetadataQueryHandler getPrescriptionMetadataQueryHandler,
            IRevokePrescriptionCommandHandler revokePrescriptionCommandHandler)
        {
            _addPharmaceuticalPrescriptionCommandHandler = addPharmaceuticalPrescriptionCommandHandler;
            _getOpenedPharmaceuticalPrescriptionQueryHandler = getOpenedPharmaceuticalPrescriptionQueryHandler;
            _getPharmaceuticalPrescriptionQueryHandler = getPharmaceuticalPrescriptionQueryHandler;
            _getPrescriptionMetadataQueryHandler = getPrescriptionMetadataQueryHandler;
            _revokePrescriptionCommandHandler = revokePrescriptionCommandHandler;
        }

        public Task<string> AddPrescription(AddPharmaceuticalPrescriptionCommand query, CancellationToken token)
        {
            return _addPharmaceuticalPrescriptionCommandHandler.Handle(query, token);
        }

        public Task<ICollection<string>> GetOpenedPrescriptions(GetOpenedPharmaceuticalPrescriptionQuery query, CancellationToken token)
        {
            return _getOpenedPharmaceuticalPrescriptionQueryHandler.Handle(query, token);
        }

        public Task<GetPharmaceuticalPrescriptionResult> GetPrescription(GetPharmaceuticalPrescriptionQuery query, CancellationToken token)
        {
            return _getPharmaceuticalPrescriptionQueryHandler.Handle(query, token);
        }

        public Task<MetadataResult> GetMetadata(CancellationToken token)
        {
            return _getPrescriptionMetadataQueryHandler.Handle(token);
        }

        public Task RevokePrescription(RevokePrescriptionCommand command, CancellationToken token)
        {
            return _revokePrescriptionCommandHandler.Handle(command, token);
        }
    }
}
