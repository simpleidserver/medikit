// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application.Queries;
using Medikit.Api.EHealth.Application.MedicinalProduct.Queries;
using Medikit.Api.EHealth.Application.MedicinalProduct.Queries.Results;
using System.Threading.Tasks;

namespace Medikit.Api.EHealth.Application.MedicinalProduct
{
    public class MedicinalProductService : IMedicinalProductService
    {
        private readonly IMediator _mediator;

        public MedicinalProductService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<SearchQueryResult<MedicinalPackageResult>> Search(SearchMedicinalPackageQuery query)
        {
            return _mediator.Send(query);
        }
    }
}
