// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application.Metadata;
using Medikit.Api.Common.Application.Queries;
using Medikit.Api.EHealth.Application.MedicinalProduct.Queries.Results;
using Medikit.EHealth.EHealthServices;
using Medikit.EHealth.EHealthServices.Parameters;
using Medikit.EHealth.EHealthServices.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.EHealth.Application.MedicinalProduct.Queries.Handlers
{
    public class SearchMedicinalPackageHandler : IRequestHandler<SearchMedicinalPackageQuery, SearchQueryResult<MedicinalPackageResult>>
    {
        private readonly IEHealthAmpService _ampService;

        public SearchMedicinalPackageHandler(IEHealthAmpService ampService)
        {
            _ampService = ampService;
        }

        public async Task<SearchQueryResult<MedicinalPackageResult>> Handle(SearchMedicinalPackageQuery query, CancellationToken token)
        {
            var result = await _ampService.SearchMedicinalPackage(new SearchAmpRequest
            {
                Count = query.Count,
                StartIndex = query.StartIndex,
                DeliveryEnvironment = query.DeliveryEnvironment.Code,
                IsCommercialised = query.IsCommercialised,
                ProductName = query.SearchText
            }, token);
            return new SearchQueryResult<MedicinalPackageResult>
            {
                Count = result.Count,
                StartIndex = result.StartIndex,
                Content = result.Content.Select(_ => new MedicinalPackageResult
                {
                    LeafletUrlLst = Convert(_.LeafletUrlLst),
                    SpcUrlLst = Convert(_.SpcUrlLst),
                    CrmUrlLst = Convert(_.CrmUrlLst),
                    Code = _.DeliveryMethods.First().Code,
                    Price = _.DeliveryMethods.First().Price,
                    Reimbursable = _.DeliveryMethods.First().Reimbursable,
                    Names = Convert(_.PrescriptionNames)
                }).ToList()
            };
        }

        private static ICollection<TranslationResult> Convert(ICollection<EHealthTranslationResult> translations)
        {
            return translations.Select(_ => new TranslationResult
            {
                Language = _.Language,
                Value = _.Value
            }).ToList();
        }
    }
}
