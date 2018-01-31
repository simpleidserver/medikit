// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Reference.Queries.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Reference
{
    public interface IReferenceTableService
    {
        Task<IEnumerable<string>> GetAllCodes();
        Task<ReferenceTableResult> GetByCode(string code, string language = null);
    }
}
