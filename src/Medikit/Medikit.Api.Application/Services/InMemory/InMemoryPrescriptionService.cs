// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Services.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Services.InMemory
{
    public class InMemoryPrescriptionService : IPrescriptionService
    {
        private readonly ICollection<PharmaceuticalPrescription> _prescriptions;

        public InMemoryPrescriptionService()
        {
            _prescriptions = new List<PharmaceuticalPrescription>
            {
                new PharmaceuticalPrescription
                {
                    CreateDateTime = DateTime.UtcNow,
                    EndExecutionDate = DateTime.UtcNow,
                    Id = "Y5YFMZPG",
                    PatientNiss = "071089",
                    PrescriptionType = PrescriptionTypes.P0,
                    Medications = new List<PharmaceuticalPrescriptionMedication>
                    {
                        new PharmaceuticalPrescriptionMedication
                        {
                            InstructionForPatient = "instruction",
                            InstructionForReimbursement = "reimbursement",
                            PackageCode = "2933901",
                            Posology = new PharmaceuticalPrescriptionFreeTextPosology
                            {
                                Content = "7 fois par jour"
                            }
                        }
                    }
                }
            };
        }

        public Task<ICollection<string>> GetOpenedPrescriptions(GetOpenedPrescriptionsParameter parameter, CancellationToken token)
        {
            ICollection<string> result = _prescriptions.Select(p => p.Id).ToList();
            return Task.FromResult(result);
        }

        public Task<PharmaceuticalPrescription> GetPrescription(GetPrescriptionParameter parameter, CancellationToken token)
        {
            return Task.FromResult(_prescriptions.FirstOrDefault(p => p.Id == parameter.PrescriptionId));
        }
    }
}
