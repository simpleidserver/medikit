// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// using Medikit.Api.Application.Domains;
using MediatR;
using Medikit.Api.Common.Application.Queries;
using Medikit.Api.EHealth.Application.MedicinalProduct.Queries.Results;
using Medikit.EHealth.Enums;

namespace Medikit.Api.EHealth.Application.MedicinalProduct.Queries
{
    public class SearchMedicinalPackageQuery : IRequest<SearchQueryResult<MedicinalPackageResult>>
    {
        public SearchMedicinalPackageQuery()
        {
            DeliveryEnvironment = DeliveryEnvironments.Public;
            IsCommercialised = true;
            StartIndex = 0;
            Count = 10;
        }

        public string SearchText { get; set; }
        public DeliveryEnvironments DeliveryEnvironment { get; set; }
        public bool? IsCommercialised { get; set; }
        public int StartIndex { get; set; }
        public int Count { get; set; }
        public string Language { get; set; }
    }
}
