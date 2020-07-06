// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.AspNetCore.Extensions;
using Medikit.Api.EHealth.Application.MedicinalProduct;
using Medikit.Api.EHealth.Application.MedicinalProduct.Queries;
using Medikit.EHealth.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Medikit.Api.AspNetCore.Controllers
{
    [Route(MedikitApiConstants.RouteNames.MedicinalProducts)]
    public class MedicinalProductsController : Controller
    {
        private readonly IMedicinalProductService _nomenclatureService;

        public MedicinalProductsController(IMedicinalProductService nomenclatureService)
        {
            _nomenclatureService = nomenclatureService;
        }

        [HttpGet("packages/.search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                var query = HttpContext.Request.Query.ToEnumerable();
                var searchResult = await _nomenclatureService.Search(BuildRequest(query));
                return new OkObjectResult(searchResult.ToDto());
            }
            catch(TaskCanceledException)
            {
                return this.ToError(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("internal", "Timeout exception")
                }, HttpStatusCode.InternalServerError, HttpContext.Request);
            }
        }

        private static SearchMedicinalPackageQuery BuildRequest(IEnumerable<KeyValuePair<string, object>> query)
        {
            var result = new SearchMedicinalPackageQuery();
            int startIndex;
            int count;
            string searchText;
            bool isCommercialised;
            int deliveryEnvironmentInt;
            if (query.TryGet("start_index", out startIndex))
            {
                result.StartIndex = startIndex;
            }

            if (query.TryGet("count", out count))
            {
                result.Count = count;
            }

            if (query.TryGet("search_text", out searchText))
            {
                result.SearchText = searchText;
            }

            if (query.TryGet("is_commercialised", out isCommercialised))
            {
                result.IsCommercialised = isCommercialised;
            }

            if (query.TryGet("delivery_environment", out deliveryEnvironmentInt))
            {
                if (Enumeration.TryParse(deliveryEnvironmentInt, out DeliveryEnvironments deliveryEnv))
                {
                    result.DeliveryEnvironment = deliveryEnv;
                }
            }

            return result;
        }
    }
}
