// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.AspNetCore.Extensions;
using Medikit.Api.Common.Application.Exceptions;
using Medikit.Api.Medicalfile.Application.Exceptions;
using Medikit.Api.Medicalfile.Application.Medicalfile;
using Medikit.Api.Patient.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.AspNetCore.Controllers
{
    [Route(MedikitApiConstants.RouteNames.Medicalfiles)]
    public class MedicalfilesController : Controller
    {
        private readonly IMedicalfileService _medicalFileService;


        public MedicalfilesController(IMedicalfileService medicalFileService)
        {
            _medicalFileService = medicalFileService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] JObject jObj)
        {
            try
            {
                var result = await _medicalFileService.AddMedicalfile(jObj.ToAddMedicalfileCommand("admin"), CancellationToken.None);
                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Content = result.ToDto().ToString(),
                    ContentType = "application/json"
                };
            }
            catch(UnknownPatientException ex)
            {
                return this.ToError(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(MedikitApiConstants.ErrorKeys.Parameter, ex.Message)
                }, HttpStatusCode.NotFound, HttpContext.Request);
            }
            catch(BadRequestException ex)
            {
                return this.ToError(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(MedikitApiConstants.ErrorKeys.Parameter, ex.Message)
                }, HttpStatusCode.BadRequest, HttpContext.Request);
            }
        }

        [HttpGet(".search")]
        public async Task<IActionResult> Search()
        {
            var query = HttpContext.Request.Query.ToEnumerable().ToSearchMedicalfileQuery();
            var searchResult = await _medicalFileService.SearchMedicalfiles(query, CancellationToken.None);
            return new OkObjectResult(searchResult.ToDto());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var result = await _medicalFileService.GetMedicalfile(id, CancellationToken.None);
                return new OkObjectResult(result.ToDto());
            }
            catch (UnknownMedicalfileException)
            {
                return new NotFoundResult();
            }
        }
    }
}
