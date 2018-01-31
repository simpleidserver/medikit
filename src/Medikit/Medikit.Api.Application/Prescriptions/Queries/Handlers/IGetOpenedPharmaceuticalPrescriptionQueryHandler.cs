// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Prescriptions.Queries.Handlers
{
    public interface IGetOpenedPharmaceuticalPrescriptionQueryHandler
    {
        Task<ICollection<string>> Handle(GetOpenedPharmaceuticalPrescriptionQuery query, CancellationToken token);
    }
}
