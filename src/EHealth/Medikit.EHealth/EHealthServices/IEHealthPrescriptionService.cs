// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.EHealthServices.Parameters;
using Medikit.EHealth.EHealthServices.Results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.EHealth.EHealthServices
{
    public interface IEHealthPrescriptionService
    {
        Task<ICollection<string>> GetOpenedPrescriptions(GetOpenedPrescriptionsParameter parameter, CancellationToken token);
        Task<PharmaceuticalPrescriptionResult> GetPrescription(GetPrescriptionParameter parameter, CancellationToken token);
    }
}
