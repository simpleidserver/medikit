// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.MedicinalProduct.Queries.Results;
using System.Threading.Tasks;

namespace Medikit.Api.Application.MedicinalProduct.Queries.Handlers
{
    public interface ISearchMedicinalProductHandler
    {
        Task<SearchQueryResult<MedicinalProductResult>> Handle(SearchMedicinalProduct query);
    }
}
