// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Common;
using Medikit.Api.Application.MedicinalProduct.Queries.Results;
using Medikit.Api.Application.Services;
using Medikit.Api.Application.Services.Parameters;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.MedicinalProduct.Queries.Handlers
{
    public class SearchMedicinalPackageHandler : ISearchMedicinalPackageHandler
    {
        private readonly IAmpService _ampService;

        public SearchMedicinalPackageHandler(IAmpService ampService)
        {
            _ampService = ampService;
        }

        public async Task<SearchQueryResult<MedicinalPackageResult>> Handle(SearchMedicinalPackage query)
        {
            var result = await _ampService.SearchMedicinalPackage(new SearchAmpRequest
            {
                Count = query.Count,
                StartIndex = query.StartIndex,
                DeliveryEnvironment = query.DeliveryEnvironment.Name,
                IsCommercialised = query.IsCommercialised,
                ProductName = query.SearchText
            }, CancellationToken.None);
            return new SearchQueryResult<MedicinalPackageResult>
            {
                Count = result.Count,
                StartIndex = result.StartIndex,
                Content = result.Content.Select(r => new MedicinalPackageResult
                {
                    LeafletUrlLst = r.LeafletUrlLst,
                    SpcUrlLst = r.SpcUrlLst,
                    CrmUrlLst = r.CrmUrlLst,
                    Code = r.DeliveryMethods.First().Code,
                    Price = r.DeliveryMethods.First().Price,
                    Reimbursable = r.DeliveryMethods.First().Reimbursable,
                    Names = r.PrescriptionNames.Select(n =>
                        new TranslationResult
                        {
                            Language = n.Language,
                            Value = n.Value
                        }
                    ).ToList()
                }).ToList()
            };
        }
    }
}
