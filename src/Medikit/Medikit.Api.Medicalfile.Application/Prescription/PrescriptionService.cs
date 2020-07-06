// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application.Metadata;
using Medikit.Api.Medicalfile.Application.Prescription.Commands;
using Medikit.Api.Medicalfile.Application.Prescription.Queries;
using Medikit.Api.Medicalfile.Prescription.Prescription.Results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Prescription
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IMediator _mediator;

        public PrescriptionService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<string> AddPrescription(AddPharmaceuticalPrescriptionCommand query, CancellationToken token)
        {
            return _mediator.Send(query, token);
        }

        public Task<ICollection<string>> GetOpenedPrescriptions(GetOpenedPharmaceuticalPrescriptionQuery query, CancellationToken token)
        {
            return _mediator.Send(query, token);
        }

        public Task<GetPharmaceuticalPrescriptionResult> GetPrescription(GetPharmaceuticalPrescriptionQuery query, CancellationToken token)
        {
            return _mediator.Send(query, token);
        }

        public Task<MetadataResult> GetMetadata(CancellationToken token)
        {
            return _mediator.Send(new GetPharmaceuticalPrescriptionMetadataQuery(), token);
        }

        public Task<bool> RevokePrescription(RevokePrescriptionCommand command, CancellationToken token)
        {
            return _mediator.Send(command, token);
        }
    }
}
