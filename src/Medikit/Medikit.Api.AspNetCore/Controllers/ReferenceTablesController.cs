// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.AspNetCore.Extensions;
using Medikit.Api.EHealth.Application.KMEHRReference;
using Medikit.Api.EHealth.Application.KMEHRReference.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Medikit.Api.AspNetCore.Controllers
{
    [Route(MedikitApiConstants.RouteNames.ReferenceTables)]
    public class ReferenceTablesController : Controller
    {
        private readonly IKMEHRReferenceTableService _referenceTableService;

        public ReferenceTablesController(IKMEHRReferenceTableService referenceTableService)
        {
            _referenceTableService = referenceTableService;
        }

        public async Task<IActionResult> Get()
        {
            var result = await _referenceTableService.GetAllCodes();
            return new OkObjectResult(result);
        }

        [HttpGet("{code}/{language?}")]
        public async Task<IActionResult> Get(string code, string language)
        {
            try
            {
                var result = await _referenceTableService.GetByCode(code, language);
                return new OkObjectResult(result.ToDto());
            }
            catch(UnknownKMEHRReferenceTableException)
            {
                return new NotFoundResult();
            }
        }
    }
}
