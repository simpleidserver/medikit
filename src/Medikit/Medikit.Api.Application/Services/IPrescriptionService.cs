// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Services.Parameters;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Services
{
    public interface IPrescriptionService
    {
        Task<ICollection<string>> GetOpenedPrescriptions(GetOpenedPrescriptionsParameter parameter, CancellationToken token);
        Task<PharmaceuticalPrescription> GetPrescription(GetPrescriptionParameter parameter, CancellationToken token);
    }
}
