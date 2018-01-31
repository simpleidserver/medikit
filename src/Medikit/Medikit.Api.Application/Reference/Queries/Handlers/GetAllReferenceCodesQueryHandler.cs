// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Medikit.Api.Application.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Reference.Queries.Handlers
{
    public class GetAllReferenceCodesQueryHandler : IGetAllReferenceCodesQueryHandler
    {
        private readonly IReferenceTableQueryRepository _referenceTableQueryRepository;

        public GetAllReferenceCodesQueryHandler(IReferenceTableQueryRepository referenceTableQueryRepository)
        {
            _referenceTableQueryRepository = referenceTableQueryRepository;
        }

        public Task<IEnumerable<string>> Handle(GetAllReferenceCodesQuery getAllReferenceCodesQuery)
        {
            return _referenceTableQueryRepository.GetAllCodes();
        }
    }
}
