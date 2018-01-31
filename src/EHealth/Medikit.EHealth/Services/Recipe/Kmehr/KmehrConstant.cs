// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public class KmehrConstant
    {
        public static class ReferenceVersion
        {
            public const string ID_KMEHR_VERSION = "1.0";
            public const string CD_PHARMACEUTICAL_PRESCRIPTION_VERSION = "2.3";
            public const string CD_TRANSACTION_VERSION = "1.13";
            public const string CD_ADDRESS_VERSION = "1.1";
            public const string CD_FEDICT_COUNTRY_CODE_VERSION = "1.2";
            public const string CD_TELECOM_TYPE_VERSION = "1.0";
            public const string CD_HCPARTY_VERSION = "1.15";
            public const string CD_STANDARD_VERSION = "1.31";
            public const string CD_SEX_VERSION = "1.1";
            public const string CD_HEADING_VERSION = "1.2";
            public const string CD_LIFECYCLE_VERSION = "1.9";
            public const string CD_UNIT_VERSION = "1.7";
        }

        public static class ReferenceNames
        {
            public const string SEX = "CD-SEX";
            public const string LIFECYCLE = "CD-LIFECYCLE";
            public const string UNIT = "CD-UNIT";
        }

        public static class HealthCarePartTypeNames
        {
            public const string APPLICATION = "application";
        }

        public static class HeadingTypeNames
        {
            public const string PRESCRIPTION = "prescription";
            public const string MEDICATION = "medication";
        }

        public static class TransactionNames
        {
            public const string PHARMACEUTICAL_PRESCRIPTION = "pharmaceuticalprescription";
        }
    }
}
