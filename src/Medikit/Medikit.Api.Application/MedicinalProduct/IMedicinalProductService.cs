// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.MedicinalProduct.Queries;
using Medikit.Api.Application.MedicinalProduct.Queries.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Api.Application.MedicinalProduct
{
    public interface IMedicinalProductService
    {
        Task<SearchQueryResult<MedicinalProductResult>> Search(SearchMedicinalProduct query);
    }
}
