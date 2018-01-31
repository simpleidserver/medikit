// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Persistence
{
    public interface IPatientQueryRepository
    {
        Task<PatientAggregate> GetByNiss(string niss, CancellationToken token);
    }
}
