// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Medicalfile.Application.Domains;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Persistence
{
    public interface IMedicalfileCommandRepository
    {
        Task Add(MedicalfileAggregate medicalfile, CancellationToken token);
        Task Commit(CancellationToken token);
    }
}
