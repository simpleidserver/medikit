// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Exceptions;
using Medikit.Api.Application.Patient.Queries.Results;
using Medikit.Api.Application.Persistence;
using Medikit.Api.Application.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Patient.Queries.Handlers
{
    public class GetPatientByNissQueryHandler : IGetPatientByNissQueryHandler
    {
        private readonly IPatientQueryRepository _patientQueryRepository;

        public GetPatientByNissQueryHandler(IPatientQueryRepository patientQueryRepository)
        {
            _patientQueryRepository = patientQueryRepository;
        }

        public async Task<PatientResult> Handle(GetPatientByNissQuery query, CancellationToken token)
        {
            var result = await _patientQueryRepository.GetByNiss(query.Niss, token);
            if (result == null)
            {
                throw new UnknownPatientException(query.Niss, string.Format(Global.UnknownPatient, query.Niss));
            }

            return new PatientResult
            {
                Birthdate = result.BirthDate,
                Firstname = result.Firstname,
                Lastname = result.Lastname,
                Niss = result.NationalIdentityNumber,
                LogoUrl = result.LogoUrl,
                CreateDateTime = result.CreateDateTime,
                UpdateDateTime = result.UpdateDateTime
            };
        }
    }
}
