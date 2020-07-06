// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.EHealth.Application.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Api.EHealth.Application.Persistence
{
    public interface IKMEHRReferenceTableQueryRepository
    {
        Task<KMEHRReferenceTable> GetByCode(string code, string language = null);
        Task<IEnumerable<string>> GetAllCodes();
    }
}