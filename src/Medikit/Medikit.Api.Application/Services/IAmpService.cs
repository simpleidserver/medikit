// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Services.Parameters;
using Medikit.Api.Application.Services.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Services
{
    public interface IAmpService
    {
        Task<SearchResult<AmpResult>> SearchByMedicinalPackageName(SearchAmpRequest request, CancellationToken token);
        Task<AmpResult> SearchByCnkCode(string deliveryEnvironment, string cnk, CancellationToken token);
    }
}
