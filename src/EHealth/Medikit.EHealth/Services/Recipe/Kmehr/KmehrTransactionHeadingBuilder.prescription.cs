// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrTransactionHeadingBuilder
    {
        public KmehrTransactionHeadingBuilder NewPrescriptionHeading(string id)
        {
            var heading = new headingType
            {
                id = new IDKMEHR[1]
                {
                    new IDKMEHR
                    {
                        SV = KmehrConstant.ReferenceVersion.ID_KMEHR_VERSION,
                        S = IDKMEHRschemes.IDKMEHR,
                        Value = id
                    }
                },
                cd = new CDHEADING[1]
                {
                    new CDHEADING
                    {
                        S = CDHEADINGschemes.CDHEADING,
                        SV = KmehrConstant.ReferenceVersion.CD_HEADING_VERSION,
                        Value = KmehrConstant.HeadingTypeNames.PRESCRIPTION
                    }
                },
                Items = null
            };
            _obj = heading;
            return this;
        }
    }
}
