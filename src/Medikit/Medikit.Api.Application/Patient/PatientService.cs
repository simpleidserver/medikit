// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Common;
using Medikit.Api.Application.Patient.Commands;
using Medikit.Api.Application.Patient.Commands.Handlers;
using Medikit.Api.Application.Patient.Queries;
using Medikit.Api.Application.Patient.Queries.Handlers;
using Medikit.Api.Application.Patient.Queries.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Patient
{
    public class PatientService : IPatientService
    {
        private readonly IGetPatientByNissQueryHandler _getPatientByNissQueryHandler;
        private readonly IAddPatientCommandHandler _addPatientCommandHandler;
        private readonly ISearchPatientsQueryHandler _searchPatientsQueryHandler;
        private readonly IGetPatientByIdQueryHandler _getPatientByIdQueryHandler;

        public PatientService(IGetPatientByNissQueryHandler getPatientByNissQueryHandler, IAddPatientCommandHandler addPatientCommandHandler, ISearchPatientsQueryHandler searchPatientsQueryHandler, IGetPatientByIdQueryHandler getPatientByIdQueryHandler)
        {
            _getPatientByNissQueryHandler = getPatientByNissQueryHandler;
            _addPatientCommandHandler = addPatientCommandHandler;
            _searchPatientsQueryHandler = searchPatientsQueryHandler;
            _getPatientByIdQueryHandler = getPatientByIdQueryHandler;
        }

        public Task<PatientResult> GetPatientByNiss(GetPatientByNissQuery query, CancellationToken token)
        {
            return _getPatientByNissQueryHandler.Handle(query, token);
        }

        public Task<string> AddPatient(AddPatientCommand command, CancellationToken token)
        {
            return _addPatientCommandHandler.Handle(command, token);
        }

        public Task<PagedResult<PatientResult>> Search(SearchPatientsQuery query, CancellationToken token)
        {
            return _searchPatientsQueryHandler.Handle(query, token);
        }

        public Task<PatientResult> GetPatientById(GetPatientByIdQuery query, CancellationToken token)
        {
            return _getPatientByIdQueryHandler.Handle(query, token);
        }
    }
}
