// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Patient.Application.Domains;
using Medikit.Api.Patient.Application.Queries.Results;
using System.Linq;
using static Medikit.Api.Patient.Application.Queries.Results.GetPatientQueryResult;

namespace Medikit.Api.Patient.Application.Extensions
{
    public static class ResultExtensions
    {
        public static GetPatientQueryResult ToResult(this PatientAggregate patient)
        {
            return new GetPatientQueryResult
            {
                Id = patient.Id,
                Birthdate = patient.BirthDate,
                Firstname = patient.Firstname,
                Lastname = patient.Lastname,
                Niss = patient.NationalIdentityNumber,
                LogoUrl = patient.LogoUrl,
                CreateDateTime = patient.CreateDateTime,
                UpdateDateTime = patient.UpdateDateTime,
                EidCardNumber = patient.EidCardNumber,
                EidCardValidity = patient.EidCardValidity,
                Gender = patient.Gender,
                ContactInformations = patient.ContactInformations.Select(_ => new ContactInformationResult
                {
                    Type = _.Type,
                    Value = _.Value
                }).ToList(),
                Address = patient.Address == null ? new AddressResult() : new AddressResult
                {
                    Coordinates = patient.Address.Coordinates,
                    Country = patient.Address.Country,
                    PostalCode = patient.Address.PostalCode,
                    Street = patient.Address.Street,
                    StreetNumber = patient.Address.StreetNumber
                }
            };
        }
    }
}
