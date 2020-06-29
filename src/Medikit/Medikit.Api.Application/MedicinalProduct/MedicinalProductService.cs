// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.MedicinalProduct.Queries;
using Medikit.Api.Application.MedicinalProduct.Queries.Handlers;
using Medikit.Api.Application.MedicinalProduct.Queries.Results;
using System.Threading.Tasks;

namespace Medikit.Api.Application.MedicinalProduct
{
    public class MedicinalProductService : IMedicinalProductService
    {
        private readonly ISearchMedicinalPackageHandler _searchMedicinalProductHandler;

        public MedicinalProductService(ISearchMedicinalPackageHandler searchMedicinalProductHandler)
        {
            _searchMedicinalProductHandler = searchMedicinalProductHandler;
        }

        public Task<SearchQueryResult<MedicinalPackageResult>> Search(SearchMedicinalPackage query)
        {
            return _searchMedicinalProductHandler.Handle(query);
        }
    }
}
