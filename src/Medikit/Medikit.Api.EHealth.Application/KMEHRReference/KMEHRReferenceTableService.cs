// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.EHealth.Application.KMEHRReference.Queries;
using Medikit.Api.EHealth.Application.KMEHRReference.Queries.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Api.EHealth.Application.KMEHRReference
{
    public class KMEHRReferenceTableService : IKMEHRReferenceTableService
    {
        private readonly IMediator _mediator;

        public KMEHRReferenceTableService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<IEnumerable<string>> GetAllCodes()
        {
            return _mediator.Send(new GetAllKMEHReferenceCodesQuery());
        }

        public Task<KMEHRReferenceTableResult> GetByCode(string code, string language = null)
        {
            return _mediator.Send(new GetKMEHRReferenceByCodeQuery(code, language));
        }
    }
}
