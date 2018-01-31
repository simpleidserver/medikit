// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Prescriptions.Queries;
using Medikit.Api.Application.Prescriptions.Results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Prescriptions
{
    public interface IPharmaceuticalPrescriptionService
    {
        Task<ICollection<string>> GetOpenedPrescriptions(GetOpenedPharmaceuticalPrescriptionQuery query, CancellationToken token);
        Task<GetPharmaceuticalPrescriptionResult> GetPrescription(GetPharmaceuticalPrescriptionQuery query, CancellationToken token);
    }
}
