// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrHealthCarePartyBuilder
    {
        public KmehrHealthCarePartyBuilder NewApplication(string name = null)
        {
            _hcParty = BuildApplication(name);
            return this;
        }

        private static hcpartyType BuildApplication(string name = null)
        {
            var hcPartyType = new hcpartyType
            {
                cd = new CDHCPARTY[1]
                {
                    new CDHCPARTY
                    {
                        SV = KmehrConstant.ReferenceVersion.CD_HCPARTY_VERSION,
                        S = CDHCPARTYschemes.CDHCPARTY,
                        Value = KmehrConstant.HealthCarePartTypeNames.APPLICATION
                    }
                }
            };
            if (!string.IsNullOrWhiteSpace(name))
            {
                hcPartyType.ItemsElementName = new ItemsChoiceType[1]
                {
                    ItemsChoiceType.name
                };
                hcPartyType.Items = new string[1]
                {
                    name
                };
            }

            return hcPartyType;
        }
    }
}
