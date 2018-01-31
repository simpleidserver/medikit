﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Reference.Queries.Handlers
{
    public interface IGetAllReferenceCodesQueryHandler
    {
        Task<IEnumerable<string>> Handle(GetAllReferenceCodesQuery getAllReferenceCodesQuery);
    }
}
