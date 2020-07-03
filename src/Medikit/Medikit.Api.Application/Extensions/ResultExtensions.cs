// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Patient.Queries.Results;
using System.Linq;

namespace Medikit.Api.Application.Extensions
{
    public static class ResultExtensions
    {
        public static PatientResult ToResult(this PatientAggregate patient)
        {
            return new PatientResult
            {
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
