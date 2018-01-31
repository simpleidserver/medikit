// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Persistence.InMemory
{
    public class InMemoryPatientQueryRepository : IPatientQueryRepository
    {
        private readonly ConcurrentBag<PatientAggregate> _patients;

        public InMemoryPatientQueryRepository(ConcurrentBag<PatientAggregate> patients)
        {
            _patients = patients;
        }

        public Task<PatientAggregate> GetByNiss(string niss, CancellationToken token)
        {
            return Task.FromResult(_patients.FirstOrDefault(p => p.NationalIdentityNumber == niss));
        }
    }
}
