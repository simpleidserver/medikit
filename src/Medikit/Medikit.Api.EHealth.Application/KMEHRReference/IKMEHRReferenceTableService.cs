// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.EHealth.Application.KMEHRReference.Queries.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Api.EHealth.Application.KMEHRReference
{
    public interface IKMEHRReferenceTableService
    {
        Task<IEnumerable<string>> GetAllCodes();
        Task<KMEHRReferenceTableResult> GetByCode(string code, string language = null);
    }
}
