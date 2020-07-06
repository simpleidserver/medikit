// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Medicalfile.Application.Domains;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Persistence.InMemory
{
    public class InMemoryMedicalfileCommandRepository : IMedicalfileCommandRepository
    {
        private ConcurrentBag<MedicalfileAggregate> _medicalFiles;

        public InMemoryMedicalfileCommandRepository(ConcurrentBag<MedicalfileAggregate> medicalfiles)
        {
            _medicalFiles = medicalfiles;
        }

        public Task Add(MedicalfileAggregate medicalfile, CancellationToken token)
        {
            _medicalFiles.Add((MedicalfileAggregate)medicalfile.Clone());
            return Task.CompletedTask;
        }

        public Task Commit(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}
