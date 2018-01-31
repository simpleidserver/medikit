// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Prescriptions.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Prescriptions.Queries.Handlers
{
    public interface IGetPharmaceuticalPrescriptionQueryHandler
    {
        Task<GetPharmaceuticalPrescriptionResult> Handle(GetPharmaceuticalPrescriptionQuery query, CancellationToken token);
    }
}
