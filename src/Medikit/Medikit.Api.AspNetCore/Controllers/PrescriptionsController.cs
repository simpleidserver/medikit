// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Exceptions;
using Medikit.Api.Application.Prescriptions;
using Medikit.Api.Application.Prescriptions.Queries;
using Medikit.Api.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.AspNetCore.Controllers
{
    [Route("prescriptions")]
    public class PrescriptionsController
    {
        private readonly IPharmaceuticalPrescriptionService _pharmaceuticalPrescriptionService;

        public PrescriptionsController(IPharmaceuticalPrescriptionService pharmaceuticalPrescriptionService)
        {
            _pharmaceuticalPrescriptionService = pharmaceuticalPrescriptionService;
        }

        [HttpPost("opened")]
        public async Task<IActionResult> GetOpenedPrescriptions([FromBody] JObject jObj)
        {
            var query = BuildGetOpenedPrescriptionsParameter(jObj);
            var result = await _pharmaceuticalPrescriptionService.GetOpenedPrescriptions(query, CancellationToken.None);
            return new OkObjectResult(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetPrescription(string id, [FromBody] JObject jObj)
        {
            try
            {
                var query = BuildGetPrescriptionParameter(id, jObj);
                var result = await _pharmaceuticalPrescriptionService.GetPrescription(query, CancellationToken.None);
                return new OkObjectResult(result.ToDto());
            }
            catch(UnknownPrescriptionException)
            {
                return new NotFoundResult();
            }
        }

        private static GetOpenedPharmaceuticalPrescriptionQuery BuildGetOpenedPrescriptionsParameter(JObject jObj)
        {
            var result = new GetOpenedPharmaceuticalPrescriptionQuery();
            string assertionToken;
            string patientNiss;
            int pageNumber;
            var values = jObj.ToObject<Dictionary<string, string>>();
            if (values.TryGet("assertion_token", out assertionToken))
            {
                result.AssertionToken = assertionToken;    
            }

            if (values.TryGet("patient_niss", out patientNiss))
            {
                result.PatientNiss = patientNiss;
            }
            
            if(values.TryGet("page_number", out pageNumber))
            {
                result.PageNumber = pageNumber;
            }

            return result;
        }

        private static GetPharmaceuticalPrescriptionQuery BuildGetPrescriptionParameter(string id, JObject jObj)
        {
            var result = new GetPharmaceuticalPrescriptionQuery
            {
                PrescriptionId = id
            };
            string assertionToken;
            var values = jObj.ToObject<Dictionary<string, string>>();
            if (values.TryGet("assertion_token", out assertionToken))
            {
                result.AssertionToken = assertionToken;
            }

            return result;
        }
    }
}