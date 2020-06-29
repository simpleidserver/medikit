// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.DICS.Request;
using Medikit.EHealth.Services.DICS.Response;
using Medikit.EHealth.SOAP.DTOs;
using System.Threading.Tasks;

namespace Medikit.EHealth.Services.DICS
{
    public interface IDicsService
    {
        Task FindAmpp(DICSFindAmppRequest request);
        Task<SOAPEnvelope<DICSFindAmpResponseBody>> FindAmp(DICSFindAmpRequest request);
        Task FindReimbursement(DICSFindReimbursementRequest request);
    }
}
