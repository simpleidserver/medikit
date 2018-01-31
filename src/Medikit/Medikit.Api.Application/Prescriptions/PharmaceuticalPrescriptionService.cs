// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
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

        public PharmaceuticalPrescriptionService(IAddPharmaceuticalPrescriptionCommandHandler addPharmaceuticalPrescriptionCommandHandler, IGetOpenedPharmaceuticalPrescriptionQueryHandler getOpenedPharmaceuticalPrescriptionQueryHandler, IGetPharmaceuticalPrescriptionQueryHandler getPharmaceuticalPrescriptionQueryHandler)
        {
            _addPharmaceuticalPrescriptionCommandHandler = addPharmaceuticalPrescriptionCommandHandler;
            _getOpenedPharmaceuticalPrescriptionQueryHandler = getOpenedPharmaceuticalPrescriptionQueryHandler;
            _getPharmaceuticalPrescriptionQueryHandler = getPharmaceuticalPrescriptionQueryHandler;
        }

        public Task<ICollection<string>> GetOpenedPrescriptions(GetOpenedPharmaceuticalPrescriptionQuery query, CancellationToken token)
        {
            return _getOpenedPharmaceuticalPrescriptionQueryHandler.Handle(query, token);
        }

        public Task<GetPharmaceuticalPrescriptionResult> GetPrescription(GetPharmaceuticalPrescriptionQuery query, CancellationToken token)
        {
            return _getPharmaceuticalPrescriptionQueryHandler.Handle(query, token);
        }
    }
}
