// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Queries;
using Medikit.Api.EHealth.Application.MedicinalProduct.Queries;
using Medikit.Api.EHealth.Application.MedicinalProduct.Queries.Results;
using System.Threading.Tasks;

namespace Medikit.Api.EHealth.Application.MedicinalProduct
{
    public interface IMedicinalProductService
    {
        Task<SearchQueryResult<MedicinalPackageResult>> Search(SearchMedicinalPackageQuery query);
    }
}
