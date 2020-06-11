// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Prescriptions.Queries.Handlers
{
    public interface IGetPrescriptionMetadataQueryHandler
    {
        Task<MetadataResult> Handle(CancellationToken token);
    }
}
