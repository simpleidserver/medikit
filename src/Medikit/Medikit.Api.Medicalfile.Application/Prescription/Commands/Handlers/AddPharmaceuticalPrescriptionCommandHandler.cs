// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.Medicalfile.Application.Persistence;
using Medikit.Api.Medicalfile.Application.Resources;
using Medikit.Api.Patient.Application.Exceptions;
using Medikit.EHealth;
using Medikit.EHealth.Enums;
using Medikit.EHealth.Exceptions;
using Medikit.EHealth.Extensions;
using Medikit.EHealth.KeyStore;
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.Services.Recipe;
using Medikit.EHealth.Services.Recipe.Kmehr;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Medikit.Api.Medicalfile.Application.Prescription.Commands.AddPharmaceuticalPrescriptionCommand;

namespace Medikit.Api.Medicalfile.Application.Prescription.Commands.Handlers
{
    public class AddPharmaceuticalPrescriptionCommandHandler : IRequestHandler<AddPharmaceuticalPrescriptionCommand, string>
    {
        private Dictionary<string, string> MAPPING_CLAIM_TO_HCPARTY = new Dictionary<string, string>
        {
            { MedicalProfessions.Doctor.Code, "persphysician" }
        };
        private const string DEFAULT_LANGUAGE = "fr";
        private readonly EHealthOptions _options;
        private readonly IMedicalfileQueryRepository _medicalfileQueryRepository;
        private readonly IKeyStoreManager _keyStoreManager;
        private readonly IRecipeService _recipeService;

        public AddPharmaceuticalPrescriptionCommandHandler(IOptions<EHealthOptions> options, IMedicalfileQueryRepository medicalfileQueryRepository, IKeyStoreManager keyStoreManager, IRecipeService recipeService)
        {
            _options = options.Value;
            _medicalfileQueryRepository = medicalfileQueryRepository;
            _keyStoreManager = keyStoreManager;
            _recipeService = recipeService;
        }

        public async Task<string> Handle(AddPharmaceuticalPrescriptionCommand command, CancellationToken token)
        {
            SAMLAssertion assertion;
            var medicalfile = await _medicalfileQueryRepository.Get(command.MedicalfileId, token);
            if (medicalfile == null)
            {
                throw new UnknownPatientException(command.MedicalfileId, string.Format(Global.UnknownMedicalFile, command.MedicalfileId));
            }

            try
            {
                assertion = SAMLAssertion.Deserialize(command.AssertionToken);
            }
            catch
            {
                throw new BadAssertionTokenException(Global.BadAssertionToken);
            }

            var createDateTime = DateTime.UtcNow;
            if (command.CreateDateTime != null)
            {
                createDateTime = command.CreateDateTime.Value;
            }

            var niss = assertion.AttributeStatement.Attribute.First(_ => _.AttributeNamespace == EHealth.Constants.AttributeStatementNamespaces.Identification).AttributeValue;
            var profession = assertion.AttributeStatement.Attribute.First(_ => _.AttributeNamespace == EHealth.Constants.AttributeStatementNamespaces.Certified).AttributeName;
            var cbe = _keyStoreManager.GetOrgAuthCertificate().Certificate.ExtractCBE();
            var msgType = new KmehrMessageBuilder()
                .AddSender((_) =>
                {
                    _.AddHealthCareParty((_) =>
                    {
                        _.AddOrganization(cbe, "application", _options.ProductName);
                    });
                    _.AddHealthCareParty((_) =>
                    {
                        _.AddPerson(niss, MAPPING_CLAIM_TO_HCPARTY[profession], string.Empty, string.Empty);
                    });
                })
                .AddRecipient((_) =>
                {
                    _.AddHealthCareParty((s) =>
                    {
                        s.AddOrganization("RECIPE", "orgpublichealth", "Recip-e");
                    });
                })
                .AddFolder("1", (_) =>
                {
                    _.New(medicalfile.PatientNiss, medicalfile.PatientLastname, new string[] { medicalfile.PatientFirstname });
                }, (_) =>
                {
                    _.AddTransaction((tr) =>
                    {
                        tr.NewPharmaceuticalPrescriptionTransaction("1", createDateTime, true, true, command.ExpirationDateTime)
                            .AddAuthor(niss, MAPPING_CLAIM_TO_HCPARTY[profession], string.Empty, string.Empty)
                            .AddTransactionHeading((h) =>
                        {
                            h.NewPrescriptionHeading("1");
                            foreach (var pharmaPrescription in command.Medications)
                            {
                                h.AddMedicationTransactionItem((ti) =>
                                {
                                    ti.SetMedicinalProduct(pharmaPrescription.PackageCode, string.Empty);
                                    if (pharmaPrescription.Posology.Type.Code == PosologyTypes.FreeText.Code)
                                    {
                                        var freeText = pharmaPrescription.Posology as AddPosologyFreeTextCommand;
                                        ti.SetPosologyFreeText(freeText.Content, DEFAULT_LANGUAGE);
                                    }
                                    else
                                    {
                                        // TODO : MANAGE STRUCTURED POSOLOGY.
                                    }

                                    if (pharmaPrescription.BeginMoment != null)
                                    {
                                        ti.SetBeginMoment(pharmaPrescription.BeginMoment.Value);
                                    }

                                    if (!string.IsNullOrWhiteSpace(pharmaPrescription.InstructionForPatient))
                                    {
                                        ti.SetInstructionForPatient(pharmaPrescription.InstructionForPatient, DEFAULT_LANGUAGE);
                                    }

                                    if (!string.IsNullOrWhiteSpace(pharmaPrescription.InstructionForReimbursement))
                                    {
                                        ti.SetInstructionForReimbursement(pharmaPrescription.InstructionForReimbursement, DEFAULT_LANGUAGE);
                                    }
                                });
                            }
                        });
                    });
                })
                .Build(createDateTime);
            var result = await _recipeService.CreatePrescription(Enum.GetName(typeof(PrescriptionTypes), command.PrescriptionType), medicalfile.PatientNiss, command.ExpirationDateTime.Value, msgType, assertion);
            return result.RID;
        }
    }
}