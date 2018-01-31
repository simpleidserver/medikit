// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using System.Collections.Generic;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrHealthCarePartyBuilder
    {
        public KmehrHealthCarePartyBuilder NewHealthCareWorker(string healthCarePartyType, string hcPartyId = null, string niss = null, string firstName = null, string familyName = null)
        {
            _hcParty = BuildHealthCareWorker(healthCarePartyType, hcPartyId, niss, firstName, familyName);
            return this;
        }

        private static hcpartyType BuildHealthCareWorker(string healthCarePartyType, string hcPartyId = null, string niss = null, string firstName = null, string familyName = null)
        {
            var hcPartyType = new hcpartyType
            {
                cd = new CDHCPARTY[1]
                {
                    new CDHCPARTY
                    {
                        SV = KmehrConstant.ReferenceVersion.CD_HCPARTY_VERSION,
                        S = CDHCPARTYschemes.CDHCPARTY,
                        Value = healthCarePartyType
                    }
                }
            };
            var ids = new List<IDHCPARTY>();
            if (!string.IsNullOrWhiteSpace(hcPartyId))
            {
                ids.Add(new IDHCPARTY
                {
                    SV = KmehrConstant.ReferenceVersion.ID_KMEHR_VERSION,
                    S = IDHCPARTYschemes.IDHCPARTY,
                    Value = hcPartyId
                });
            }

            if (!string.IsNullOrWhiteSpace(niss))
            {
                ids.Add(new IDHCPARTY
                {
                    SV = KmehrConstant.ReferenceVersion.ID_KMEHR_VERSION,
                    S = IDHCPARTYschemes.INSS,
                    Value = niss
                });
            }

            hcPartyType.id = ids.ToArray();
            var itemChoiceTypes = new List<ItemsChoiceType>();
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                itemChoiceTypes.Add(ItemsChoiceType.firstname);
            }

            if (!string.IsNullOrWhiteSpace(familyName))
            {
                itemChoiceTypes.Add(ItemsChoiceType.familyname);
            }

            hcPartyType.Items = new string[itemChoiceTypes.Count];
            hcPartyType.ItemsElementName = new ItemsChoiceType[itemChoiceTypes.Count];
            int i = 0;
            foreach (var itemChoiceType in itemChoiceTypes)
            {
                switch (itemChoiceType)
                {
                    case ItemsChoiceType.firstname:
                        hcPartyType.Items[i] = firstName;
                        break;
                    case ItemsChoiceType.familyname:
                        hcPartyType.Items[i] = familyName;
                        break;
                }

                i++;
            }

            return hcPartyType;
        }
    }
}
