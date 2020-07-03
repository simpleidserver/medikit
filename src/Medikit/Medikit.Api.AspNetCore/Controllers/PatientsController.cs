// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Exceptions;
using Medikit.Api.Application.Patient;
using Medikit.Api.Application.Patient.Queries;
using Medikit.Api.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.AspNetCore.Controllers
{
    [Route(MedikitApiConstants.RouteNames.Patients)]
    public class PatientsController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] JObject jObj)
        {
            // TODO : Get prescriberid from identity token.
            var result = await _patientService.AddPatient(jObj.ToAddPatientCommand("admin"), CancellationToken.None);
            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.Created,
                Content = JsonConvert.SerializeObject(new { id = result }),
                ContentType = "application/json"
            };
        }

        [HttpGet(".search")]
        public async Task<IActionResult> Search()
        {
            var query = HttpContext.Request.Query.ToEnumerable();
            var searchResult = await _patientService.Search(BuildSearchRequest(query), CancellationToken.None);
            return new OkObjectResult(searchResult.ToDto(Request.GetAbsoluteUriWithVirtualPath()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var result = await _patientService.GetPatientById(new GetPatientByIdQuery(id), CancellationToken.None);
                return new OkObjectResult(result.ToDto(Request.GetAbsoluteUriWithVirtualPath()));
            }
            catch(UnknownPatientException)
            {
                return new NotFoundResult();
            }
        }

        [HttpGet("niss/{niss}")]
        public async Task<IActionResult> GetByNiss(string niss)
        {
            try
            {
                var result = await _patientService.GetPatientByNiss(new GetPatientByNissQuery(niss), CancellationToken.None);
                return new OkObjectResult(result.ToDto(Request.GetAbsoluteUriWithVirtualPath()));
            }
            catch(UnknownPatientException)
            {
                return new NotFoundResult();
            }
        }

        private static SearchPatientsQuery BuildSearchRequest(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            string niss, firstname, lastname;
            var result = new SearchPatientsQuery();
            result.ExtractSearchParameters(parameters);
            if (parameters.TryGet("niss", out niss))
            {
                result.Niss = niss;
            }

            if (parameters.TryGet("firstname", out firstname))
            {
                result.Firstname = firstname;
            }

            if (parameters.TryGet("lastname", out lastname))
            {
                result.Lastname = lastname;
            }

            return result;
        }
    }
}
