// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application;
using Medikit.Api.Application.Common;
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.MedicinalProduct.Queries.Results;
using Medikit.Api.Application.Metadata;
using Medikit.Api.Application.Patient.Commands;
using Medikit.Api.Application.Patient.Queries.Results;
using Medikit.Api.Application.Prescriptions.Results;
using Newtonsoft.Json.Linq;
using System;
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

        public static JObject ToDto(this PatientResult patient, string baseUrl)
        {
            var result = new JObject
            {
                { MedikitApiConstants.PatientNames.BirthDate, patient.Birthdate },
                { MedikitApiConstants.PatientNames.Firstname, patient.Firstname },
                { MedikitApiConstants.PatientNames.Lastname, patient.Lastname },
                { MedikitApiConstants.PatientNames.EidCardNumber, patient.EidCardNumber },
                { MedikitApiConstants.PatientNames.Gender, (int)patient.Gender },
                { MedikitApiConstants.PatientNames.Niss, patient.Niss },
                { MedikitApiConstants.PatientNames.CreateDateTime, patient.CreateDateTime },
                { MedikitApiConstants.PatientNames.UpdateDateTime, patient.UpdateDateTime },
                { MedikitApiConstants.PatientNames.ContactInformations, new JArray(patient.ContactInformations.Select(_ => ToDto(_))) },
                { MedikitApiConstants.PatientNames.Address, patient.Address.ToDto() }
            };
            if (patient.EidCardValidity != null)
            {
                result.Add(MedikitApiConstants.PatientNames.EidCardValidity, patient.EidCardValidity.Value);
            }

            if (!string.IsNullOrWhiteSpace(patient.LogoUrl))
            {
                result.Add(MedikitApiConstants.PatientNames.LogoUrl, $"{baseUrl}/{patient.LogoUrl}");
            }

            return result;
        }

        public static JObject ToDto(this AddressResult address)
        {
            return new JObject
            {
                { MedikitApiConstants.AddressNames.Country, address.Country },
                { MedikitApiConstants.AddressNames.PostalCode, address.PostalCode },
                { MedikitApiConstants.AddressNames.Street, address.Street },
                { MedikitApiConstants.AddressNames.StreetNumber, address.StreetNumber },
                { MedikitApiConstants.AddressNames.Coordinates, new JArray(address.Coordinates) }
            };
        }

        public static AddPatientCommand ToAddPatientCommand(this JObject jObj, string prescriberId)
        {
            var result = new AddPatientCommand
            {
                PrescriberId = prescriberId
            };
            var values = jObj.ToObject<Dictionary<string, object>>();
            if (values.TryGet(MedikitApiConstants.PatientNames.Firstname, out string firstname))
            {
                result.Firstname = firstname;
            }

            if (values.TryGet(MedikitApiConstants.PatientNames.Lastname, out string lastname))
            {
                result.Lastname = lastname;
            }

            if (values.TryGet(MedikitApiConstants.PatientNames.Niss, out string niss))
            {
                result.NationalIdentityNumber = niss;
            }

            if (values.TryGet(MedikitApiConstants.PatientNames.Gender, out GenderTypes gender))
            {
                result.Gender = gender;
            }

            if (values.TryGet(MedikitApiConstants.PatientNames.BirthDate, out DateTime birthDate))
            {
                result.BirthDate = birthDate;
            }

            if (values.TryGet(MedikitApiConstants.PatientNames.Base64EncodedImage, out string base64EncodedImage))
            {
                result.Base64EncodedImage = base64EncodedImage;
            }

            if (values.TryGet(MedikitApiConstants.PatientNames.EidCardNumber, out string eidCardNumber))
            {
                result.EidCardNumber = eidCardNumber;
            }

            if (values.TryGet(MedikitApiConstants.PatientNames.EidCardValidity, out DateTime eidCardValidity))
            {
                result.EidCardValidity = eidCardValidity;
            }

            if (values.ContainsKey(MedikitApiConstants.PatientNames.Address))
            {
                var address = new AddPatientCommand.Address();
                var addressDic = ((JObject)values[MedikitApiConstants.PatientNames.Address]).ToObject<Dictionary<string, object>>();
                if (addressDic.TryGet(MedikitApiConstants.AddressNames.Street, out string street))
                {
                    address.Street = street;
                }

                if (addressDic.TryGet(MedikitApiConstants.AddressNames.StreetNumber, out int streetNumber))
                {
                    address.StreetNumber = streetNumber;
                }

                if (addressDic.TryGet(MedikitApiConstants.AddressNames.Country, out string country))
                {
                    address.Country = country;
                }

                if (addressDic.TryGet(MedikitApiConstants.AddressNames.PostalCode, out string postalCode))
                {
                    address.PostalCode = postalCode;
                }

                if (addressDic.TryGetValue(MedikitApiConstants.AddressNames.Coordinates, out object coordinates))
                {
                    var coords = new List<double>();
                    var jArr = coordinates as JArray;
                    if (jArr != null)
                    {
                        foreach(var r in jArr)
                        {
                            if (double.TryParse(r.ToString(), out double d))
                            {
                                coords.Add(d);
                            }
                        }
                    }

                    address.Coordinates = coords;
                }

                result.PatientAddress = address;
            }

            if (values.ContainsKey(MedikitApiConstants.PatientNames.ContactInformations))
            {
                var jArr = values[MedikitApiConstants.PatientNames.ContactInformations] as JArray;
                var contactInfos = new List<AddPatientCommand.ContactInformation>();
                foreach (JObject o in jArr)
                {
                    var ci = new AddPatientCommand.ContactInformation();
                    var dic = o.ToObject<Dictionary<string, object>>();
                    if (dic.TryGet(MedikitApiConstants.ContactInfoNames.Type, out ContactInformationTypes type))
                    {
                        ci.Type = type;
                    }

                    if (dic.TryGet(MedikitApiConstants.ContactInfoNames.Value, out string value))
                    {
                        ci.Value = value;
                    }

                    contactInfos.Add(ci);
                }

                result.ContactInformations = contactInfos;
            }

            return result;
        }

        public static JObject ToDto(this ContactInformationResult contactInfo)
        {
            return new JObject
            {
                { MedikitApiConstants.ContactInfoNames.Type, (int)contactInfo.Type },
                { MedikitApiConstants.ContactInfoNames.Value, contactInfo.Value }
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

        public static JObject ToDto(this PagedResult<PatientResult> search, string baseUrl)
        {
            return new JObject
            {
                { "count", search.Count },
                { "start_index", search.StartIndex },
                { "total_length", search.TotalLength },
                { "content", new JArray(search.Content.Select(_ => _.ToDto(baseUrl))) }
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