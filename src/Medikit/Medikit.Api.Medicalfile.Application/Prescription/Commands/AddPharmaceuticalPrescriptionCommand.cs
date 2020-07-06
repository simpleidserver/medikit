// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.EHealth.Enums;
using System;
using System.Collections.Generic;

namespace Medikit.Api.Medicalfile.Application.Prescription.Commands
{
    public class AddPharmaceuticalPrescriptionCommand : IRequest<string>
    {
        public AddPharmaceuticalPrescriptionCommand()
        {
            Medications = new List<AddMedicationCommand>();
        }

        public string AssertionToken { get; set; }
        public string PatientNiss { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? ExpirationDateTime { get; set; }
        public PrescriptionTypes PrescriptionType { get; set; }
        public ICollection<AddMedicationCommand> Medications { get; set; }

        public class AddMedicationCommand
        {
            public string PackageCode { get; set; }
            public string InstructionForPatient { get; set; }
            public string InstructionForReimbursement { get; set; }
            public DateTime? BeginMoment { get; set; }
            public AddPosologyCommand Posology { get; set; }
        }

        public class AddPosologyCommand
        {
            public AddPosologyCommand(PosologyTypes posologyType)
            {
                Type = posologyType;
            }

            public PosologyTypes Type { get; private set; }
        }
        
        public class AddPosologyFreeTextCommand : AddPosologyCommand
        {

            public AddPosologyFreeTextCommand() : base(PosologyTypes.FreeText) { }

            public string Content { get; set; }
        }
    }
}
