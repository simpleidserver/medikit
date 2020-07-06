// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.EHealthServices.Parameters;
using Medikit.EHealth.EHealthServices.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.EHealth.EHealthServices
{
    public interface IEHealthAmpService
    {
        Task<SearchEHealthQueryResult<AmppResult>> SearchMedicinalPackage(SearchAmpRequest request, CancellationToken token);
        Task<AmpResult> SearchByCnkCode(string deliveryEnvironment, string cnk, CancellationToken token);
    }
}
