// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Prescriptions.Queries.Handlers
{
    public class GetPrescriptionMetadataQueryHandler : IGetPrescriptionMetadataQueryHandler
    {
        private readonly IMetadataResultBuilder _metadataResultBuilder;

        public GetPrescriptionMetadataQueryHandler(IMetadataResultBuilder metadataResultBuilder)
        {
            _metadataResultBuilder = metadataResultBuilder;
        }

        public Task<MetadataResult> Handle(CancellationToken token)
        {
            return _metadataResultBuilder.AddTranslatedEnum<PrescriptionTypes>("prescriptionTypes").Build("en", token);
        }
    }
}
