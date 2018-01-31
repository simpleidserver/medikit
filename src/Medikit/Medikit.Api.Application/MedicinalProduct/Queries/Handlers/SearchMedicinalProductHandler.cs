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
    public class SearchMedicinalProductHandler : ISearchMedicinalProductHandler
    {
        private readonly IAmpService _ampService;

        public SearchMedicinalProductHandler(IAmpService ampService)
        {
            _ampService = ampService;
        }

        public async Task<SearchQueryResult<MedicinalProductResult>> Handle(SearchMedicinalProduct query)
        {
            var result = await _ampService.SearchByMedicinalPackageName(new SearchAmpRequest
            {
                Count = query.Count,
                StartIndex = query.StartIndex,
                DeliveryEnvironment = query.DeliveryEnvironment.Name,
                IsCommercialised = query.IsCommercialised,
                ProductName = query.SearchText
            }, CancellationToken.None);
            return new SearchQueryResult<MedicinalProductResult>
            {
                Count = result.Count,
                StartIndex = result.StartIndex,
                Content = result.Content.Select(r => new MedicinalProductResult
                {
                    Code = r.Code,
                    OfficialName = r.OfficialName,
                    Packages = r.AmppLst.Select(n => new MedicinalPackageResult
                    {
                        DeliveryMethods = n.DeliveryMethods.Select(d => new MedicinalDeliveryMethod
                        {
                            Code = d.Code,
                            CodeType = d.CodeType,
                            DeliveryEnvironment = d.DeliveryEnvironment
                        }).ToList(),
                        PrescriptionNames = n.PrescriptionNames.Select(p => new TranslationResult
                        {
                            Language = p.Language,
                            Value = p.Value
                        }).ToList()
                    }).ToList(),
                    Names = r.Names.Select(n =>
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
