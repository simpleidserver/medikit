// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Extensions;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Persistence.InMemory
{
    public class InMemoryPatientCommandRepository : IPatientCommandRepository
    {
        private readonly ConcurrentBag<PatientAggregate> _patients;

        public InMemoryPatientCommandRepository(ConcurrentBag<PatientAggregate> patients)
        {
            _patients = patients;
        }

        public Task<bool> Add(PatientAggregate patient, CancellationToken token)
        {
            _patients.Add((PatientAggregate)patient.Clone());
            return Task.FromResult(true);
        }

        public Task<bool> Delete(string id, CancellationToken token)
        {
            _patients.Remove(_patients.First(p => p.Id == id));
            return Task.FromResult(true);
        }

        public async Task<bool> Update(PatientAggregate patient, CancellationToken token)
        {
            await Delete(patient.Id, token);
            await Add(patient, token);
            return true;
        }

        public Task<bool> Commit(CancellationToken token)
        {
            return Task.FromResult(true);
        }
    }
}
