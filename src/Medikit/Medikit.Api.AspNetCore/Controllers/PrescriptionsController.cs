// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Exceptions;
using Medikit.Api.Application.Prescriptions;
using Medikit.Api.Application.Prescriptions.Commands;
using Medikit.Api.Application.Prescriptions.Queries;
using Medikit.Api.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.AspNetCore.Controllers
{
    [Route("prescriptions")]
    public class PrescriptionsController : Controller
    {
        private readonly IPharmaceuticalPrescriptionService _pharmaceuticalPrescriptionService;

        public PrescriptionsController(IPharmaceuticalPrescriptionService pharmaceuticalPrescriptionService)
        {
            _pharmaceuticalPrescriptionService = pharmaceuticalPrescriptionService;
        }

        [HttpGet("metadata")]
        public async Task<IActionResult> GetMetadata()
        {
            var result = await _pharmaceuticalPrescriptionService.GetMetadata(CancellationToken.None);
            return new OkObjectResult(result.ToDto());
        }

        [HttpPost("opened")]
        public async Task<IActionResult> GetOpenedPrescriptions([FromBody] JObject jObj)
        {
            var query = BuildGetOpenedPrescriptionsParameter(jObj);
            var result = await _pharmaceuticalPrescriptionService.GetOpenedPrescriptions(query, CancellationToken.None);
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription([FromBody] JObject jObj)
        {
            var query = BuildAddPharmaceuticalPrescription(jObj);
            var result = await _pharmaceuticalPrescriptionService.AddPrescription(query, CancellationToken.None);
            return new CreatedResult("", new { id = result });
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
            catch (UnknownPrescriptionException)
            {
                return new NotFoundResult();
            }
        }

        [HttpPost("{rid}/revoke")]
        public async Task<IActionResult> RevokePrescription(string rid, [FromBody] JObject jObj)
        {
            await _pharmaceuticalPrescriptionService.RevokePrescription(BuildRevokePrescriptionCommand(rid, jObj), CancellationToken.None);
            return new NoContentResult();
        }

        private static RevokePrescriptionCommand BuildRevokePrescriptionCommand(string rid, JObject jObj)
        {
            var result = new RevokePrescriptionCommand { Rid = rid };
            string assertionToken;
            string reason;
            var values = jObj.ToObject<Dictionary<string, object>>();
            if (values.TryGet("assertion_token", out assertionToken))
            {
                result.AssertionToken = assertionToken;
            }

            if (values.TryGet("reason", out reason))
            {
                result.Reason = reason;
            }

            return result;
        }

        private static GetOpenedPharmaceuticalPrescriptionQuery BuildGetOpenedPrescriptionsParameter(JObject jObj)
        {
            var result = new GetOpenedPharmaceuticalPrescriptionQuery();
            string assertionToken;
            string patientNiss;
            int pageNumber;
            var values = jObj.ToObject<Dictionary<string, object>>();
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
            var values = jObj.ToObject<Dictionary<string, object>>();
            if (values.TryGet("assertion_token", out assertionToken))
            {
                result.AssertionToken = assertionToken;
            }

            return result;
        }

        private static AddPharmaceuticalPrescriptionCommand BuildAddPharmaceuticalPrescription(JObject jObj)
        {
            string assertionToken, niss;
            PrescriptionTypes prescriptionType;
            DateTime createDateTime;
            DateTime expirationDateTime;
            var values = jObj.ToObject<Dictionary<string, object>>();
            var result = new AddPharmaceuticalPrescriptionCommand();
            if (values.TryGet("assertion_token", out assertionToken))
            {
                result.AssertionToken = assertionToken;
            }

            if (values.TryGet("niss", out niss))
            {
                result.PatientNiss = niss;
            }

            if (values.TryGet("create_datetime", out createDateTime))
            {
                result.CreateDateTime = createDateTime;
            }

            if (values.TryGet("expiration_datetime", out expirationDateTime))
            {
                result.ExpirationDateTime = expirationDateTime;
            }

            if (values.TryGet("prescription_type", out prescriptionType))
            {
                result.PrescriptionType = prescriptionType;
            }

            var medications = jObj.SelectToken("medications") as JArray;
            if (medications != null)
            {
                foreach(JObject medication in medications)
                {
                    var medicationDic = medication.ToObject<Dictionary<string, object>>();
                    DateTime beginMoment;
                    string packageCode, instructionForPatient, instructionForReimbursement;
                    var newMedication = new AddPharmaceuticalPrescriptionMedication();
                    if (medicationDic.TryGet("package_code", out packageCode))
                    {
                        newMedication.PackageCode = packageCode;
                    }

                    if (medicationDic.TryGet("instruction_for_patient", out instructionForPatient))
                    {
                        newMedication.InstructionForPatient = instructionForPatient;
                    }

                    if (medicationDic.TryGet("instruction_for_reimbursement", out instructionForReimbursement))
                    {
                        newMedication.InstructionForReimbursement = instructionForReimbursement;
                    }

                    if (medicationDic.TryGet("begin_moment", out beginMoment))
                    {
                        newMedication.BeginMoment = beginMoment;
                    }

                    var posology = medication.SelectToken("posology") as JObject;
                    if (posology != null)
                    {
                        var posologyType = posology.SelectToken("type").ToString();
                        if (posologyType != null)
                        {
                            if (posologyType == "freetext")
                            {
                                newMedication.Posology = new PharmaceuticalPrescriptionFreeTextPosology
                                {
                                    Content = posology.SelectToken("value").ToString()
                                };
                            }
                        }
                    }

                    result.Medications.Add(newMedication);
                }
            }

            return result;
        }
    }
}