// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.EHealth.Application.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.EHealth.Application.KMEHRReference.Queries.Handlers
{
    public class GetAllKMEHRReferenceCodesQueryHandler : IRequestHandler<GetAllKMEHReferenceCodesQuery, IEnumerable<string>>
    {
        private readonly IKMEHRReferenceTableQueryRepository _referenceTableQueryRepository;

        public GetAllKMEHRReferenceCodesQueryHandler(IKMEHRReferenceTableQueryRepository referenceTableQueryRepository)
        {
            _referenceTableQueryRepository = referenceTableQueryRepository;
        }

        public Task<IEnumerable<string>> Handle(GetAllKMEHReferenceCodesQuery request, CancellationToken cancellationToken)
        {
            return _referenceTableQueryRepository.GetAllCodes();
        }
    }
}
