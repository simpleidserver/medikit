// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.CIVICS.Request;
using System.Threading.Tasks;

namespace Medikit.EHealth.Services.CIVICS
{
    public interface ICIVICSService
    {
        Task FindCNK(CIVICSFindCNKRequest request);
    }
}
