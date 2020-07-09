// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.EHealthServices.Parameters;
using Medikit.EHealth.EHealthServices.Results;
using Medikit.EHealth.Services.Recipe.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.EHealth.EHealthServices
{
    public interface IEHealthPrescriptionService
    {
        Task<ListOpenRidsResult> GetOpenedPrescriptions(GetPrescriptionsParameter parameter, CancellationToken token);
        Task<ListRidsHistoryResult> GetHistoryPrescriptions(GetPrescriptionsParameter parameter, CancellationToken token);
        Task<PharmaceuticalPrescriptionResult> GetPrescription(GetPrescriptionParameter parameter, CancellationToken token);
    }
}
