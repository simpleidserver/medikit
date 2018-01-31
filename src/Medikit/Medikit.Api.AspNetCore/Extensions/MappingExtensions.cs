// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application;
using Medikit.Api.Application.Common;
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.MedicinalProduct.Queries.Results;
using Medikit.Api.Application.Prescriptions.Results;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Medikit.Api.AspNetCore.Extensions
{
    public static class MappingExtensions
    {
        #region Prescription

        public static JObject ToDto(this GetPharmaceuticalPrescriptionResult getPharmaceuticalPrescriptionResult)
        {
            var result = new JObject
            {
                { "id", getPharmaceuticalPrescriptionResult.Id },
                { "create_datetime", getPharmaceuticalPrescriptionResult.CreateDateTime },
                { "end_execution_datetime", getPharmaceuticalPrescriptionResult.EndExecutionDate },
                { "prescription_type", getPharmaceuticalPrescriptionResult.PrescriptionType },
                { "patient", getPharmaceuticalPrescriptionResult.Patient.ToDto() },
                { "prescriber", getPharmaceuticalPrescriptionResult.Prescriber.ToDto() },
                { "medications", new JArray(getPharmaceuticalPrescriptionResult.Medications.Select(_ => ToDto(_)).ToList()) }
            };
            return result;
        }

        public static JObject ToDto(this GetPharmaceuticalPrescriptionPersonResult person)
        {
            var result = new JObject
            {
                { "firstname", person.Firstname },
                { "lastname", person.Lastname },
                { "birthdate", person.Birthdate }
            };
            return result;
        }

        public static JObject ToDto(this GetPharmaceuticalPrescriptionPatientResult patient)
        {
            var result = ToDto((GetPharmaceuticalPrescriptionPersonResult)patient);
            result.Add("niss", patient.Niss);
            return result;
        }

        public static JObject ToDto(this GetPharmaceuticalPrescriptionPrescriberResult prescriber)
        {
            var result = ToDto((GetPharmaceuticalPrescriptionPersonResult)prescriber);
            result.Add("inami", prescriber.INAMINumber);
            return result;
        }

        public static JObject ToDto(this GetPharmaceuticalPrescriptionMedication medication)
        {
            var result = new JObject
            {
                { "instruction_for_patient", medication.InstructionForPatient },
                { "instruction_for_reimbursement", medication.InstructionForReimbursement },
                { "package", new JObject
                {
                    { "code", medication.MedicationPackage.PackageCode },
                    { "translations", new JArray(medication.MedicationPackage.Translations.Select(_ => _.ToDto()).ToList())
                    }
                } }
            };
            if (medication.Posology != null) 
            {
                result.Add("posology", medication.Posology.ToDto());
            }

            return result;
        }

        private static JObject ToDto(this GetPharmaceuticalPrescriptionPosology posology)
        {
            if (posology.Type == PosologyTypes.FreeText.Name)
            {
                return new JObject
                {
                    { "type", PosologyTypes.FreeText.Name.ToLowerInvariant() },
                    { "content", ((GetPharmaceuticalPrescriptionPosologyFreeText)posology).Content }
                };
            }

            return null;
        }

        #endregion

        #region Medicinal product

        public static JObject ToDto(this SearchQueryResult<MedicinalProductResult> result)
        {
            return new JObject
            {
                { "count", result.Count },
                { "start_index", result.StartIndex },
                { "content", new JArray(result.Content.Select(c => c.ToDto())) }
            };
        }

        public static JObject ToDto(this MedicinalProductResult amp)
        {
            var result = new JObject
            {
                { "code", amp.Code },
                { "official_name", amp.OfficialName }
            };
            var packages = new JArray();
            foreach (var pkg in amp.Packages)
            {
                packages.Add(new JObject
                {
                    { "delivery_methods", ToDto(pkg.DeliveryMethods) },
                    { "prescription_names", ToDto(pkg.PrescriptionNames) }
                });
            }
            result.Add("packages", packages);
            result.Add("names", ToDto(amp.Names));
            return result;
        }

        public static JArray ToDto(this ICollection<TranslationResult> translations)
        {
            var names = new JArray();
            foreach (var translation in translations)
            {
                names.Add(translation.ToDto());
            }
            return names;
        }

        public static JArray ToDto(this ICollection<MedicinalDeliveryMethod> deliveryMethods)
        {
            var result = new JArray();
            foreach (var deliveryMethod in deliveryMethods)
            {
                result.Add(new JObject
                {
                    { "code", deliveryMethod.Code },
                    { "code_type", deliveryMethod.CodeType },
                    { "delivery_environment", deliveryMethod.DeliveryEnvironment }
                });
            }

            return result;
        }

        #endregion

        public static JObject ToDto(this TranslationResult translation)
        {
            return new JObject
            {
                { "language", translation.Language },
                { "value", translation.Value }
            };
        }
    }
}