// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application;
using Medikit.Api.Application.Common;
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.MedicinalProduct.Queries.Results;
using Medikit.Api.Application.Metadata;
using Medikit.Api.Application.Patient.Queries.Results;
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

        public static JObject ToDto(this SearchQueryResult<MedicinalPackageResult> result)
        {
            return new JObject
            {
                { "count", result.Count },
                { "start_index", result.StartIndex },
                { "content", new JArray(result.Content.Select(c => c.ToDto())) }
            };
        }

        public static JObject ToDto(this MedicinalPackageResult ampp)
        {
            var result = new JObject
            {
                { "code", ampp.Code },
                { "price", ampp.Price },
                { "names", ToDto(ampp.Names) },
                { "reimbursable", ampp.Reimbursable },
                { "leafleturl", ToDto(ampp.LeafletUrlLst) },
                { "crmurl", ToDto(ampp.CrmUrlLst) },
                { "spcurl", ToDto(ampp.SpcUrlLst) }
            };
            return result;
        }

        public static JArray ToDto(this ICollection<LinkTranslationResult> translations)
        {
            var names = new JArray();
            foreach (var translation in translations)
            {
                names.Add(translation.ToDto());
            }

            return names;
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

        #region Patient 

        public static JObject ToDto(this PatientResult patient)
        {
            return new JObject
            {
                { "birthdate", patient.Birthdate },
                { "firstname", patient.Firstname },
                { "lastname", patient.Lastname },
                { "niss", patient.Niss },
                { "create_datetime", patient.CreateDateTime },
                { "update_datetime", patient.UpdateDateTime },
                { "logo_url", patient.LogoUrl }
            };
        }

        #endregion

        public static JObject ToDto(this LinkTranslationResult translation)
        {
            return new JObject
            {
                { "language", translation.Language },
                { "href", translation.Href }
            };
        }

        public static JObject ToDto(this TranslationResult translation)
        {
            return new JObject
            {
                { "language", translation.Language },
                { "value", translation.Value }
            };
        }

        public static JObject ToDto(this PagedResult<PatientResult> search)
        {
            return new JObject
            {
                { "count", search.Count },
                { "start_index", search.StartIndex },
                { "total_length", search.TotalLength },
                { "content", new JArray(search.Content.Select(_ => _.ToDto())) }
            };
        }

        public static JObject ToDto(this MetadataResult metadata)
        {
            var result = new JObject();
            foreach(var kvp in metadata.Content)
            {
                result.Add(kvp.Key, kvp.Value.ToDto());
            }

            return result;
        }

        public static JObject ToDto(this MetadataRecord record)
        {
            var result = new JObject();
            var translations = new JArray();
            var children = new JArray();
            foreach(var translation in record.Translations)
            {
                translations.Add(new JObject
                {
                    { translation.Language, translation.Value }
                });
            }

            if (record.Children != null)
            {
                foreach(var child in record.Children)
                {
                    children.Add(new JObject
                    {
                        { child.Key, child.Value.ToDto() }
                    });
                }
            }

            result.Add("translations", translations);
            result.Add("children", children);
            return result;
        }
    }
}