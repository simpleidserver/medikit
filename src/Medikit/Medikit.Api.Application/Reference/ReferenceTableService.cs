// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Reference.Queries;
using Medikit.Api.Application.Reference.Queries.Handlers;
using Medikit.Api.Application.Reference.Queries.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Reference
{
    public class ReferenceTableService : IReferenceTableService
    {
        private readonly IGetAllReferenceCodesQueryHandler _getAllReferenceCodesQueryHandler;
        private readonly IGetReferenceByCodeQueryHandler _getReferenceByCodeQueryHandler;

        public ReferenceTableService(IGetAllReferenceCodesQueryHandler getAllReferenceCodesQueryHandler, IGetReferenceByCodeQueryHandler getReferenceByCodeQueryHandler)
        {
            _getAllReferenceCodesQueryHandler = getAllReferenceCodesQueryHandler;
            _getReferenceByCodeQueryHandler = getReferenceByCodeQueryHandler;
        }

        public Task<IEnumerable<string>> GetAllCodes()
        {
            return _getAllReferenceCodesQueryHandler.Handle(new GetAllReferenceCodesQuery());
        }

        public Task<ReferenceTableResult> GetByCode(string code, string language = null)
        {
            return _getReferenceByCodeQueryHandler.Handle(new GetReferenceByCodeQuery(code, language));
        }
    }
}
