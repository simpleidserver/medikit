// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Reference;
using Medikit.Api.Application.Reference.Exceptions;
using Medikit.Api.Application.Reference.Queries.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Medikit.Api.AspNetCore.Controllers
{
    [Route("referencetables")]
    public class ReferenceTablesController : Controller
    {
        private readonly IReferenceTableService _referenceTableService;

        public ReferenceTablesController(IReferenceTableService referenceTableService)
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
                return new OkObjectResult(ToDto(result));
            }
            catch(UnknownReferenceTableException)
            {
                return new NotFoundResult();
            }
        }

        public static JObject ToDto(ReferenceTableResult referenceTable)
        {
            var result = new JObject
            {
                { "name", referenceTable.Name },
                { "code", referenceTable.Code },
                { "published_date", referenceTable.PublishedDateTime },
                { "status", referenceTable.Status },
                { "version", referenceTable.Version }
            };
            var content = new JObject();
            foreach(var record in referenceTable.Content)
            {
                var translations = new JArray();
                foreach(var translation in record.Translations)
                {
                    translations.Add(new JObject
                    {
                        { "language", translation.Language },
                        { "value", translation.Value }
                    });
                }

                var translationsAttr = new JObject
                {
                    { "translations", translations }
                };
                content.Add(record.Code, translationsAttr);
            }

            result.Add("content", content);
            return result;
        }
    }
}
