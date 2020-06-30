// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Exceptions;
using Medikit.Api.Application.Patient;
using Medikit.Api.Application.Patient.Commands;
using Medikit.Api.Application.Patient.Queries;
using Medikit.Api.Application.Patient.Queries.Results;
using Medikit.Api.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
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

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] JObject jObj)
        {
            // TODO : get prescriber id from identity token.
            var result = await _patientService.AddPatient(BuildAddPatientCommand(jObj, "admin"), CancellationToken.None);
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

        private static AddPatientCommand BuildAddPatientCommand(JObject jObj, string prescriberId)
        {
            var result = new AddPatientCommand();
            var values = jObj.ToObject<Dictionary<string, object>>();
            if (values.TryGet("firstname", out string firstname))
            {
                result.Firstname = firstname;
            }

            if (values.TryGet("lastname", out string lastname))
            {
                result.Lastname = lastname;
            }

            if (values.TryGet("niss", out string niss))
            {
                result.NationalIdentityNumber = niss;
            }

            if (values.TryGet("gender", out GenderTypes gender))
            {
                result.Gender = gender;
            }

            if (values.TryGet("birthdate", out DateTime birthDate))
            {
                result.BirthDate = birthDate;
            }

            if (values.TryGet("logo_url", out string logoUrl))
            {
                result.LogoUrl = logoUrl;
            }

            if (values.TryGet("eid_cardnumber", out string eidCardNumber))
            {
                result.EidCardNumber = eidCardNumber;
            }

            if (values.TryGet("eid_cardvalidity", out DateTime eidCardValidity))
            {
                result.EidCardValidity = eidCardValidity;
            }

            if (values.ContainsKey("addresses"))
            {
                var jArr = values["addresses"] as JArray;
                var addresses = new List<AddPatientCommand.Address>();
                foreach(JObject o in jArr)
                {
                    addresses.Add(BuildAddress(o));
                }

                result.Addresses = addresses;
            }

            if (values.ContainsKey("contactinfos"))
            {
                var jArr = values["contactinfos"] as JArray;
                var contactInfos = new List<AddPatientCommand.ContactInformation>();
                foreach (JObject o in jArr)
                {
                    contactInfos.Add(BuildContactInformation(o));
                }

                result.ContactInformations = contactInfos;
            }

            return result;
        }

        private static AddPatientCommand.Address BuildAddress(JObject jObj)
        {
            var result = new AddPatientCommand.Address();
            var values = jObj.ToObject<Dictionary<string, object>>();
            if (values.TryGet("country", out string country))
            {
                result.Country = country;
            }

            if (values.TryGet("postal_code", out string postalCode))
            {
                result.PostalCode = postalCode;
            }

            if (values.TryGet("street", out string street))
            {
                result.Street = street;
            }

            if (values.TryGet("street_number", out int streetNumber))
            {
                result.StreetNumber = streetNumber;
            }

            if (values.TryGet("box", out int box))
            {
                result.Box = box;
            }

            return result;
        }

        private static AddPatientCommand.ContactInformation BuildContactInformation(JObject jObj)
        {
            var result = new AddPatientCommand.ContactInformation();
            var values = jObj.ToObject<Dictionary<string, object>>();
            if (values.TryGet("type", out ContactInformationTypes type))
            {
                result.Type = type;
            }

            if (values.TryGet("value", out string value))
            {
                result.Value = value;
            }

            return result;
        }
    }
}
