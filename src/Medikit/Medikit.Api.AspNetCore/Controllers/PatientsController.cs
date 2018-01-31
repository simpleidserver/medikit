// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Exceptions;
using Medikit.Api.Application.Patient;
using Medikit.Api.Application.Patient.Queries;
using Medikit.Api.Application.Patient.Queries.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.AspNetCore.Controllers
{
    [Route("patients")]
    public class PatientsController
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
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
    }
}
