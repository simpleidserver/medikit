// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Exceptions;
using Medikit.Api.Application.Patient;
using Medikit.Api.Application.Patient.Queries;
using Medikit.Api.Application.Patient.Queries.Results;
using Medikit.Api.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.AspNetCore.Controllers
{
    [Route("patients")]
    public class PatientsController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet(".search")]
        public async Task<IActionResult> Search()
        {
            var query = HttpContext.Request.Query.ToEnumerable();
            var searchResult = await _patientService.Search(BuildSearchRequest(query), CancellationToken.None);
            return new OkObjectResult(searchResult.ToDto());
        }

        [HttpGet("{niss}")]
        public async Task<IActionResult> GetByNiss(string niss)
        {
            try
            {
                var result = await _patientService.GetPatientByNiss(new GetPatientByNissQuery(niss), CancellationToken.None);
                return new OkObjectResult(ToDto(result));
            }
            catch(UnknownPatientException)
            {
                return new NotFoundResult();
            }
        }

        private static JObject ToDto(PatientResult patient)
        {
            return new JObject
            {
                { "firstname", patient.Firstname },
                { "lastname", patient.Lastname },
                { "birthdate", patient.Birthdate },
                { "niss", patient.Niss }
            };
        }

        private static SearchPatientsQuery BuildSearchRequest(IEnumerable<KeyValuePair<string, string>> parameters)
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
