// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Exceptions;
using Medikit.Api.Application.Persistence;
using Medikit.Api.Application.Resources;
using Medikit.EHealth.Services.Recipe.Kmehr;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Application.Prescriptions.Commands.Handlers
{
    public class AddPharmaceuticalPrescriptionCommandHandler : IAddPharmaceuticalPrescriptionCommandHandler
    {
        private const string SAM_VERSION = "640";
        private const string DEFAULT_LANGUAGE = "fr";
        private readonly MedikitServerOptions _options;
        private readonly IPatientQueryRepository _patientQueryRepository;

        public AddPharmaceuticalPrescriptionCommandHandler(IOptions<MedikitServerOptions> options, IPatientQueryRepository patientQueryRepository)
        {
            _options = options.Value;
            _patientQueryRepository = patientQueryRepository;
        }

        public async Task Handle(AddPharmaceuticalPrescriptionCommand command, CancellationToken token)
        {
            var person = await _patientQueryRepository.GetByNiss(command.Prescription.PatientNiss, token);
            if (person == null)
            {
                throw new UnknownPatientException(command.Prescription.PatientNiss, string.Format(Global.UnknownPatient, command.Prescription.PatientNiss));
            }

            new KmehrMessageBuilder()
                .AddSender((lst) =>
                {
                    lst.AddHealthCareParty((s) =>
                    {
                        s.AddOrganization("app", "app");
                    });
                })
                .AddRecipient((lst) =>
                {
                    lst.AddHealthCareParty((s) =>
                    {
                        s.AddOrganization("app", "app");
                    });
                })
                .AddFolder("1", (p) =>
                {
                    p.New(person.NationalIdentityNumber, person.Lastname, new string[] { person.Firstname });
                }, (t) =>
                {
                    t.AddTransaction((tr) =>
                    {
                        for(var i = 0; i < command.Prescription.Medications.Count(); i++)
                        {
                            var pharmaPrescription = command.Prescription.Medications.ElementAt(i);
                            tr.NewPharmaceuticalPrescriptionTransaction(i.ToString())
                                .AddTransactionItem((ti) =>
                                {
                                    // TODO : EXTERNALIZE THE LANGUAGE.
                                    if (pharmaPrescription.Posology.Type.Id == PosologyTypes.FreeText.Id)
                                    {
                                        var freeText = pharmaPrescription.Posology as PharmaceuticalPrescriptionFreeTextPosology;
                                        ti.SetPosologyFreeText(freeText.Content, DEFAULT_LANGUAGE);
                                    }
                                    else
                                    {
                                        // TODO : MANAGE STRUCTURED POSOLOGY.
                                    }

                                    if (!string.IsNullOrWhiteSpace(pharmaPrescription.InstructionForPatient))
                                    {
                                        ti.SetInstructionForPatient(pharmaPrescription.InstructionForPatient, DEFAULT_LANGUAGE);
                                    }

                                    if (!string.IsNullOrWhiteSpace(pharmaPrescription.InstructionForReimbursement))
                                    {
                                        ti.SetInstructionForReimbursement(pharmaPrescription.InstructionForReimbursement, DEFAULT_LANGUAGE);
                                    }



                                    ti.SetMedicinalProduct(SAM_VERSION, pharmaPrescription.PackageCode, string.Empty);
                                });
                        }
                    });
                });

        }
    }
}