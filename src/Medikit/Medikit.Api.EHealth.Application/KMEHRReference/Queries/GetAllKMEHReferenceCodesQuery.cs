// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using System.Collections.Generic;

namespace Medikit.Api.EHealth.Application.KMEHRReference.Queries
{
    public class GetAllKMEHReferenceCodesQuery : IRequest<IEnumerable<string>>
    {
    }
}
