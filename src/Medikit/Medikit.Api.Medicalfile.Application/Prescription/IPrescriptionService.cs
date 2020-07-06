// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Metadata;
using Medikit.Api.Medicalfile.Application.Prescription.Commands;
using Medikit.Api.Medicalfile.Application.Prescription.Queries;
using Medikit.Api.Medicalfile.Prescription.Prescription.Results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Medicalfile.Application.Prescription
{
    public interface IPrescriptionService
    {
        Task<ICollection<string>> GetOpenedPrescriptions(GetOpenedPharmaceuticalPrescriptionQuery query, CancellationToken token);
        Task<GetPharmaceuticalPrescriptionResult> GetPrescription(GetPharmaceuticalPrescriptionQuery query, CancellationToken token);
        Task<string> AddPrescription(AddPharmaceuticalPrescriptionCommand query, CancellationToken token);
        Task<MetadataResult> GetMetadata(CancellationToken token);
        Task<bool> RevokePrescription(RevokePrescriptionCommand command, CancellationToken token);
    }
}
