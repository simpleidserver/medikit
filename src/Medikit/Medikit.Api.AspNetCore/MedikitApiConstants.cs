namespace Medikit.Api.AspNetCore
{
    public static class MedikitApiConstants
    {
        public static class RouteNames
        {
            public const string Patients = "patients";
            public const string Files = "files";
            public const string Medicalfiles = "medicalfiles";
            public const string MedicinalProducts = "medicinalproducts";
            public const string ReferenceTables = "referencetables";
            public const string Messages = "messages";
        }

        public static class SearchResultNames
        {
            public const string Count = "count";
            public const string StartIndex = "start_index";
            public const string TotalLength = "total_length";
            public const string Content = "content";
        }

        public static class SearchNames
        {
            public const string AssertionToken = "assertion_token";
            public const string PageNumber = "page_number";
            public const string HasMoreResults = "has_more_results";
            public const string Prescriptions = "prescriptions";
            public const string StartIndex = "start_index";
            public const string EndIndex = "end_index";
        }

        public static class DeleteMessageNames
        {
            public const string MessageIds = "message_ids";
        }

        public static class MessageNames
        {
            public const string Id = "id";
            public const string Destination = "destination";
            public const string Sender = "sender";
            public const string Title = "title";
            public const string ContentType = "content_type";
            public const string MimeType = "mime_type";
            public const string HasAnnex = "has_annex";
            public const string PublicationDate = "publication_date";
            public const string ExpirationDate = "expiration_date";
            public const string Size = "size";
            public const string IsImportant = "is_important";
        }

        public static class IdentityNames
        {
            public const string Id = "id";
            public const string Type = "type";
            public const string Quality = "quality";
        }

        public static class SenderNames
        {
            public const string Name = "name";
            public const string Firstname = "firstname";
        }

        public static class PrescriptionNames
        {
            public const string Rid = "rid";
            public const string Status = "status";
            public const string Reason = "reason";
            public const string MedicalfileId = "medicalfile_id";
            public const string CreateDateTime = "create_datetime";
            public const string ExpirationDateTime = "expiration_datetime";
            public const string PrescriptionType = "prescription_type";
            public const string Medications = "medications";
        }

        public static class MedicationNames
        {
            public const string InstructionForPatient = "instruction_for_patient";
            public const string InstructionForReimbursement = "instruction_for_reimbursement";
            public const string BeginMoment = "begin_moment";
            public const string Posology = "posology";
            public const string PackageCode = "package_code";
        }

        public static class PosologyNames
        {
            public const string Type = "type";
            public const string Value = "value";
        }

        public static class PatientNames
        {
            public const string Id = "id";
            public const string BirthDate = "birthdate";
            public const string Firstname = "firstname";
            public const string Lastname = "lastname";
            public const string Niss = "niss";
            public const string CreateDateTime = "create_datetime";
            public const string UpdateDateTime = "update_datetime";
            public const string LogoUrl = "logo_url";
            public const string ContactInformations = "contact_infos";
            public const string Address = "address";
            public const string Gender = "gender";
            public const string Base64EncodedImage = "base64_image";
            public const string EidCardNumber = "eid_cardnumber";
            public const string EidCardValidity = "eid_cardvalidity";
        }

        public static class MedicalfileNames
        {
            public const string Id = "id";
            public const string PatientId = "patient_id";
            public const string Niss = "niss";
            public const string Firstname = "firstname";
            public const string Lastname = "lastname";
            public const string CreateDateTime = "create_datetime";
            public const string UpdateDateTime = "update_datetime";
        }

        public static class AddressNames
        {
            public const string Country = "country";
            public const string PostalCode = "postal_code";
            public const string Street = "street";
            public const string StreetNumber = "street_number";
            public const string Coordinates = "coordinates";
        }

        public static class ContactInfoNames
        {
            public const string Type = "type";
            public const string Value = "value";
        }

        public static class ErrorKeys
        {
            public const string Parameter = "parameter";
        }
    }
}
