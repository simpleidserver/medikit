// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application.Metadata;
using Medikit.EHealth.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Prescription.Queries.Handlers
{
    public class GetPrescriptionMetadataQueryHandler : IRequestHandler<GetPharmaceuticalPrescriptionMetadataQuery, MetadataResult>
    {
        private readonly IMetadataResultBuilder _metadataResultBuilder;

        public GetPrescriptionMetadataQueryHandler(IMetadataResultBuilder metadataResultBuilder)
        {
            _metadataResultBuilder = metadataResultBuilder;
        }

        public Task<MetadataResult> Handle(GetPharmaceuticalPrescriptionMetadataQuery query, CancellationToken token)
        {
            return _metadataResultBuilder.AddTranslatedEnum<PrescriptionTypes>("prescriptionTypes").Build("en", token);
        }
    }
}
