// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Patient.Application.Domains;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Patient.Application.Persistence
{
    public interface IPatientCommandRepository
    {
        Task<bool> Add(PatientAggregate patient, CancellationToken token);
        Task<bool> Delete(string id, CancellationToken token);
        Task<bool> Update(PatientAggregate patient, CancellationToken token);
        Task<bool> Commit(CancellationToken token);
    }
}
