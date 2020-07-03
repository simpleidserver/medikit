namespace Medikit.Api.AspNetCore
{
    public static class MedikitApiConstants
    {
        public static class RouteNames
        {
            public const string Patients = "patients";
        }

        public static class PatientNames
        {
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
    }
}
