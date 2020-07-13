// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Metadata;
using Medikit.Api.Common.Application.Persistence;
using Medikit.Api.Common.Application.Queries;
using Medikit.Api.EHealth.Application.KMEHRReference.Queries.Results;
using Medikit.Api.EHealth.Application.MedicinalProduct.Queries.Results;
using Medikit.Api.EHealth.Application.Message.Queries;
using Medikit.Api.EHealth.Application.Message.Queries.Results;
using Medikit.Api.Medicalfile.Application.Medicalfile.Commands;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries;
using Medikit.Api.Medicalfile.Application.Medicalfile.Queries.Results;
using Medikit.Api.Medicalfile.Application.Prescription.Commands;
using Medikit.Api.Medicalfile.Application.Prescription.Queries;
using Medikit.Api.Medicalfile.Application.Prescription.Results;
using Medikit.Api.Medicalfile.Prescription.Prescription.Results;
using Medikit.Api.Patient.Application.Commands;
using Medikit.Api.Patient.Application.Domains;
using Medikit.Api.Patient.Application.Queries.Results;
using Medikit.EHealth.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using static Medikit.Api.Medicalfile.Application.Prescription.Commands.AddPharmaceuticalPrescriptionCommand;
using static Medikit.Api.Medicalfile.Prescription.Prescription.Results.GetPharmaceuticalPrescriptionResult;
using static Medikit.Api.Patient.Application.Queries.Results.GetPatientQueryResult;

namespace Medikit.Api.AspNetCore.Extensions
{
    public static class MappingExtensions
    {
        #region DTOs

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

        public static JObject ToDto(this PersonResult person)
        {
            var result = new JObject
            {
                { "firstname", person.Firstname },
                { "lastname", person.Lastname },
                { "birthdate", person.Birthdate }
            };
            return result;
        }

        public static JObject ToDto(this PatientResult patient)
        {
            var result = ToDto((PersonResult)patient);
            result.Add("niss", patient.Niss);
            return result;
        }

        public static JObject ToDto(this PrescriberResult prescriber)
        {
            var result = ToDto((PersonResult)prescriber);
            result.Add("inami", prescriber.INAMINumber);
            return result;
        }

        public static JObject ToDto(this MedicationResult medication)
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

        private static JObject ToDto(this PosologyResult posology)
        {
            if (posology.Type == PosologyTypes.FreeText.Code)
            {
                return new JObject
                {
                    { "type", PosologyTypes.FreeText.Code.ToLowerInvariant() },
                    { "content", ((PosologyFreeTextResult)posology).Content }
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

        public static JArray ToDto(this ICollection<TranslationResult> translations)
        {
            var names = new JArray();
            foreach (var translation in translations)
            {
                names.Add(translation.ToDto());
            }
            return names;
        }

        #endregion

        #region Patient 

        public static JObject ToDto(this PagedResult<GetPatientQueryResult> search, string baseUrl)
        {
            return new JObject
            {
                { MedikitApiConstants.SearchResultNames.Count, search.Count },
                { MedikitApiConstants.SearchResultNames.StartIndex, search.StartIndex },
                { MedikitApiConstants.SearchResultNames.TotalLength, search.TotalLength },
                { MedikitApiConstants.SearchResultNames.Content, new JArray(search.Content.Select(_ => _.ToDto(baseUrl))) }
            };
        }

        public static JObject ToDto(this GetPatientQueryResult patient, string baseUrl)
        {
            var result = new JObject
            {
                { MedikitApiConstants.PatientNames.Id, patient.Id },
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

        public static JObject ToDto(this ContactInformationResult contactInfo)
        {
            return new JObject
            {
                { MedikitApiConstants.ContactInfoNames.Type, (int)contactInfo.Type },
                { MedikitApiConstants.ContactInfoNames.Value, contactInfo.Value }
            };
        }

        #endregion

        #region KMEHR Reference

        public static JObject ToDto(this KMEHRReferenceTableResult referenceTable)
        {
            var result = new JObject
            {
                { "name", referenceTable.Name },
                { "code", referenceTable.Code },
                { "published_date", referenceTable.PublishedDateTime },
                { "status", referenceTable.Status },
                { "version", referenceTable.Version }
            };
            var content = new JObject();
            foreach (var record in referenceTable.Content)
            {
                var translations = new JArray();
                foreach (var translation in record.Translations)
                {
                    translations.Add(new JObject
                    {
                        { "language", translation.Language },
                        { "value", translation.Value }
                    });
                }

                var translationsAttr = new JObject
                {
                    { "translations", translations }
                };
                content.Add(record.Code, translationsAttr);
            }

            result.Add("content", content);
            return result;
        }

        #endregion

        #region Medical prescription

        public static JObject ToDto(this SearchPharmaceuticalPrescriptionResult search)
        {
            var result = new JObject
            {
                { MedikitApiConstants.SearchNames.HasMoreResults, search.HasMoreResults }
            };
            var jArr = new JArray();
            foreach(var prescription in search.Prescriptions)
            {
                jArr.Add(new JObject
                {
                    { MedikitApiConstants.PrescriptionNames.Rid, prescription.RID },
                    { MedikitApiConstants.PrescriptionNames.Status, prescription.Status }
                });
            }

            result.Add(MedikitApiConstants.SearchNames.Prescriptions, jArr);
            return result;
        }

        #endregion

        #region Medical file

        public static JObject ToDto(this PagedResult<GetMedicalfileResult> search)
        {
            return new JObject
            {
                { MedikitApiConstants.SearchResultNames.Count, search.Count },
                { MedikitApiConstants.SearchResultNames.StartIndex, search.StartIndex },
                { MedikitApiConstants.SearchResultNames.TotalLength, search.TotalLength },
                { MedikitApiConstants.SearchResultNames.Content, new JArray(search.Content.Select(_ => _.ToDto())) }
            };
        }

        public static JObject ToDto(this GetMedicalfileResult medicalfile)
        {
            return new JObject
            {
                { MedikitApiConstants.MedicalfileNames.Firstname, medicalfile.PatientFirstname },
                { MedikitApiConstants.MedicalfileNames.Lastname, medicalfile.PatientLastname },
                { MedikitApiConstants.MedicalfileNames.Niss, medicalfile.PatientNiss },
                { MedikitApiConstants.MedicalfileNames.PatientId, medicalfile.PatientId },
                { MedikitApiConstants.MedicalfileNames.Id, medicalfile.Id },
                { MedikitApiConstants.MedicalfileNames.CreateDateTime, medicalfile.CreateDateTime },
                { MedikitApiConstants.MedicalfileNames.UpdateDateTime, medicalfile.UpdateDateTime },
            };
        }

        #endregion

        #region Message

        public static JObject ToDto(this MessageResult message)
        {
            return new JObject
            {
                { MedikitApiConstants.MessageNames.ContentType, message.ContentType },
                { MedikitApiConstants.MessageNames.Destination, message.Destination.ToDto() },
                { MedikitApiConstants.MessageNames.ExpirationDate, message.ExpirationDate },
                { MedikitApiConstants.MessageNames.HasAnnex, message.HasAnnex },
                { MedikitApiConstants.MessageNames.Id, message.Id },
                { MedikitApiConstants.MessageNames.IsImportant, message.IsImportant },
                { MedikitApiConstants.MessageNames.MimeType, message.MimeType },
                { MedikitApiConstants.MessageNames.PublicationDate, message.PublicationDate },
                { MedikitApiConstants.MessageNames.Sender, message.Sender.ToDto() },
                { MedikitApiConstants.MessageNames.Size, message.Size },
                { MedikitApiConstants.MessageNames.Title, message.Title }
            };
        }

        public static JObject ToDto(this MessageResult.IdentityResult message)
        {
            return new JObject
            {
                { MedikitApiConstants.IdentityNames.Id, message.Id },
                { MedikitApiConstants.IdentityNames.Quality, message.Quality },
                { MedikitApiConstants.IdentityNames.Type, message.Type }
            };
        }

        public static JObject ToDto(this MessageResult.SenderResult message)
        {
            return new JObject
            {
                { MedikitApiConstants.IdentityNames.Id, message.Id },
                { MedikitApiConstants.IdentityNames.Quality, message.Quality },
                { MedikitApiConstants.IdentityNames.Type, message.Type },
                { MedikitApiConstants.SenderNames.Name, message.Name },
                { MedikitApiConstants.SenderNames.Firstname, message.FirstName }
            };
        }

        #endregion

        #region Common

        public static JObject ToDto(this MetadataResult metadata)
        {
            var result = new JObject();
            foreach (var kvp in metadata.Content)
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

        public static JObject ToDto(this TranslationResult translation)
        {
            return new JObject
            {
                { "language", translation.Language },
                { "value", translation.Value }
            };
        }

        #endregion

        #endregion

        #region Command

        public static AddPatientCommand ToAddPatientCommand(this JObject jObj)
        {
            var result = new AddPatientCommand();
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
                        foreach (var r in jArr)
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

        public static AddMedicalfileCommand ToAddMedicalfileCommand(this JObject jObj, string prescriberId)
        {
            var result = new AddMedicalfileCommand
            {
                PrescriberId = prescriberId
            };
            var values = jObj.ToObject<Dictionary<string, object>>();
            if (values.TryGet(MedikitApiConstants.MedicalfileNames.PatientId, out string patientId))
            {
                result.PatientId = patientId;
            }

            return result;
        }

        public static GetMessagesQuery ToGetMessagesQuery(this JObject jObj)
        {
            var result = new GetMessagesQuery();
            var values = jObj.ToObject<Dictionary<string, object>>();
            if (values.TryGet(MedikitApiConstants.SearchNames.AssertionToken, out string assertionToken))
            {
                result.AssertionToken = assertionToken;
            }

            if (values.TryGet(MedikitApiConstants.SearchNames.StartIndex, out int startIndex))
            {
                result.StartIndex = startIndex;
            }

            if (values.TryGet(MedikitApiConstants.SearchNames.EndIndex, out int endIndex))
            {
                result.EndIndex = endIndex;
            }

            return result;
        }

        public static SearchMedicalfileQuery ToSearchMedicalfileQuery(this IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var result = new SearchMedicalfileQuery();
            result.ExtractSearchParameters(parameters);
            if (parameters.TryGet(MedikitApiConstants.MedicalfileNames.Niss, out string niss))
            {
                result.Niss = niss;
            }

            if (parameters.TryGet(MedikitApiConstants.MedicalfileNames.Firstname, out string firstname))
            {
                result.Firstname = firstname;
            }

            if (parameters.TryGet(MedikitApiConstants.MedicalfileNames.Lastname, out string lastname))
            {
                result.Lastname = lastname;
            }

            return result;
        }

        public static GetPharmaceuticalPrescriptionsQuery ToGetPharmaceuticalPrescriptionsQuery(this JObject jObj, string medicalFileId)
        {
            var result = new GetPharmaceuticalPrescriptionsQuery
            {
                MedicalfileId = medicalFileId
            };
            var values = jObj.ToObject<Dictionary<string, object>>();
            if (values.TryGet(MedikitApiConstants.SearchNames.AssertionToken, out string assertionToken))
            {
                result.AssertionToken = assertionToken;
            }

            if (values.TryGet(MedikitApiConstants.SearchNames.PageNumber, out int pageNumber))
            {
                result.PageNumber = pageNumber;
            }

            return result;
        }

        public static GetOpenedPharmaceuticalPrescriptionsQuery ToGetOpenedPharmaceuticalPrescriptionsQuery(this JObject jObj, string medicalFileId)
        {
            var result = new GetOpenedPharmaceuticalPrescriptionsQuery
            {
                MedicalfileId = medicalFileId
            };
            var values = jObj.ToObject<Dictionary<string, object>>();
            if (values.TryGet(MedikitApiConstants.SearchNames.AssertionToken, out string assertionToken))
            {
                result.AssertionToken = assertionToken;
            }

            if (values.TryGet(MedikitApiConstants.SearchNames.PageNumber, out int pageNumber))
            {
                result.PageNumber = pageNumber;
            }

            return result;
        }

        public static GetPharmaceuticalPrescriptionQuery BuildGetPrescriptionParameter(this JObject jObj, string medicalfileid, string rid)
        {
            var result = new GetPharmaceuticalPrescriptionQuery
            {
                MedicalfileId = medicalfileid,
                PrescriptionId = rid
            };
            string assertionToken;
            var values = jObj.ToObject<Dictionary<string, object>>();
            if (values.TryGet(MedikitApiConstants.SearchNames.AssertionToken, out assertionToken))
            {
                result.AssertionToken = assertionToken;
            }

            return result;
        }


        public static RevokePrescriptionCommand BuildRevokePrescriptionCommand(this JObject jObj, string medicalfileid, string rid)
        {
            var result = new RevokePrescriptionCommand { MedicalfileId = medicalfileid, Rid = rid };
            var values = jObj.ToObject<Dictionary<string, object>>();
            if (values.TryGet(MedikitApiConstants.SearchNames.AssertionToken, out string assertionToken))
            {
                result.AssertionToken = assertionToken;
            }

            if (values.TryGet(MedikitApiConstants.PrescriptionNames.Reason, out string reason))
            {
                result.Reason = reason;
            }

            return result;
        }


        public static AddPharmaceuticalPrescriptionCommand BuildAddPharmaceuticalPrescription(this JObject jObj, string medicalfileId)
        {
            var values = jObj.ToObject<Dictionary<string, object>>();
            var result = new AddPharmaceuticalPrescriptionCommand { MedicalfileId = medicalfileId };
            if (values.TryGet(MedikitApiConstants.SearchNames.AssertionToken, out string assertionToken))
            {
                result.AssertionToken = assertionToken;
            }

            if (values.TryGet(MedikitApiConstants.PrescriptionNames.MedicalfileId, out string medicalfileid))
            {
                result.MedicalfileId = medicalfileid;
            }

            if (values.TryGet(MedikitApiConstants.PrescriptionNames.CreateDateTime, out DateTime createDateTime))
            {
                result.CreateDateTime = createDateTime;
            }

            if (values.TryGet(MedikitApiConstants.PrescriptionNames.ExpirationDateTime, out DateTime expirationDateTime))
            {
                result.ExpirationDateTime = expirationDateTime;
            }

            if (values.TryGet(MedikitApiConstants.PrescriptionNames.PrescriptionType, out PrescriptionTypes prescriptionType))
            {
                result.PrescriptionType = prescriptionType;
            }

            var medications = jObj.SelectToken(MedikitApiConstants.PrescriptionNames.Medications) as JArray;
            if (medications != null)
            {
                foreach (JObject medication in medications)
                {
                    var medicationDic = medication.ToObject<Dictionary<string, object>>();
                    var newMedication = new AddMedicationCommand();
                    if (medicationDic.TryGet(MedikitApiConstants.MedicationNames.PackageCode, out string packageCode))
                    {
                        newMedication.PackageCode = packageCode;
                    }

                    if (medicationDic.TryGet(MedikitApiConstants.MedicationNames.InstructionForPatient, out string instructionForPatient))
                    {
                        newMedication.InstructionForPatient = instructionForPatient;
                    }

                    if (medicationDic.TryGet(MedikitApiConstants.MedicationNames.InstructionForReimbursement, out string instructionForReimbursement))
                    {
                        newMedication.InstructionForReimbursement = instructionForReimbursement;
                    }

                    if (medicationDic.TryGet(MedikitApiConstants.MedicationNames.BeginMoment, out DateTime beginMoment))
                    {
                        newMedication.BeginMoment = beginMoment;
                    }

                    var posology = medication.SelectToken(MedikitApiConstants.MedicationNames.Posology) as JObject;
                    if (posology != null)
                    {
                        var posologyType = posology.SelectToken(MedikitApiConstants.PosologyNames.Type).ToString();
                        if (posologyType != null)
                        {
                            if (posologyType == "freetext")
                            {
                                newMedication.Posology = new AddPosologyFreeTextCommand
                                {
                                    Content = posology.SelectToken(MedikitApiConstants.PosologyNames.Value).ToString()
                                };
                            }
                        }
                    }

                    result.Medications.Add(newMedication);
                }
            }

            return result;
        }


        #endregion
    }
}