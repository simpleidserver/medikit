// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Common.Application.Metadata;

namespace Medikit.Api.Medicalfile.Application.Prescription.Queries
{
    public class GetPharmaceuticalPrescriptionMetadataQuery : IRequest<MetadataResult>
    {
        public string MedicalfileId { get; set; }
    }
}
