// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.AspNetCore.Extensions;
using Medikit.Api.Common.Application.Exceptions;
using Medikit.Api.Medicalfile.Application.Exceptions;
using Medikit.Api.Medicalfile.Application.Medicalfile;
using Medikit.Api.Medicalfile.Application.Prescription;
using Medikit.Api.Medicalfile.Application.Prescription.Queries;
using Medikit.Api.Patient.Application.Exceptions;
using Medikit.EHealth.Exceptions;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IPrescriptionService _prescriptionService;

        public MedicalfilesController(IMedicalfileService medicalFileService, IPrescriptionService prescriptionService)
        {
            _medicalFileService = medicalFileService;
            _prescriptionService = prescriptionService;
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

        [HttpPost("{medicalfileid}/prescriptions")]
        public async Task<IActionResult> GetPrescriptions(string medicalfileid, [FromBody] JObject jObj)
        {
            var query = jObj.ToGetPharmaceuticalPrescriptionsQuery(medicalfileid);
            var result = await _prescriptionService.GetPrescriptions(query, CancellationToken.None);
            return new OkObjectResult(result.ToDto());
        }

        [HttpPost("{medicalfileid}/prescriptions/add")]
        public async Task<IActionResult> AddPrescription(string medicalfileid, [FromBody] JObject jObj)
        {
            var query = jObj.BuildAddPharmaceuticalPrescription(medicalfileid);
            var result = await _prescriptionService.AddPrescription(query, CancellationToken.None);
            return new OkObjectResult(new { id = result });
        }

        [HttpPost("{medicalfileid}/prescriptions/opened")]
        public async Task<IActionResult> GetOpenedPrescriptions(string medicalfileid, [FromBody] JObject jObj)
        {
            var query = jObj.ToGetOpenedPharmaceuticalPrescriptionsQuery(medicalfileid);
            var result = await _prescriptionService.GetOpenedPrescriptions(query, CancellationToken.None);
            return new OkObjectResult(result.ToDto());
        }

        [HttpPost("{medicalfileid}/prescriptions/{id}")]
        public async Task<IActionResult> GetPrescription(string medicalfileId, string id, [FromBody] JObject jObj)
        {
            try
            {
                var query = jObj.BuildGetPrescriptionParameter(medicalfileId, id);
                var result = await _prescriptionService.GetPrescription(query, CancellationToken.None);
                return new OkObjectResult(result.ToDto());
            }
            catch (UnknownPrescriptionException)
            {
                return new NotFoundResult();
            }
        }

        [HttpPost("{medicalfileid}/prescriptions/{id}/revoke")]
        public async Task<IActionResult> RevokePrescription(string medicalfileid, string id, [FromBody] JObject jObj)
        {
            var query = jObj.BuildRevokePrescriptionCommand(medicalfileid, id);
            await _prescriptionService.RevokePrescription(query, CancellationToken.None);
            return new NoContentResult();
        }

        [HttpGet("{medicalfileid}/prescriptions/metadata")]
        public async Task<IActionResult> GetMetadata(string medicalfileid)
        {
            var result = await _prescriptionService.GetMetadata(new GetPharmaceuticalPrescriptionMetadataQuery { MedicalfileId = medicalfileid },  CancellationToken.None);
            return new OkObjectResult(result.ToDto());
        }
    }
}
