using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using System.Collections.Generic;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrHealthCarePartyBuilder
    {
        public KmehrHealthCarePartyBuilder AddOrganization(string id, string organizationType, string name = null)
        {
            _hcParty = BuildOrganization(id, organizationType, name);
            return this;
        }

        public KmehrHealthCarePartyBuilder AddPerson(string id, string personType, string firstname, string lastname)
        {
            _hcParty = BuildPerson(id, personType, firstname, lastname);
            return this;
        }

        private static hcpartyType BuildOrganization(string id, string organizationType, string name = null)
        {
            var hcPartyType = new hcpartyType
            {
                cd = new CDHCPARTY[1]
                {
                    new CDHCPARTY
                    {
                        SV = KmehrConstant.ReferenceVersion.CD_HCPARTY_VERSION,
                        S = CDHCPARTYschemes.CDHCPARTY,
                        Value = organizationType
                    }
                },
                id = new IDHCPARTY[1]
                {
                    new IDHCPARTY
                    {
                        SV = KmehrConstant.ReferenceVersion.ID_KMEHR_VERSION,
                        S = IDHCPARTYschemes.IDHCPARTY,
                        Value = id
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


        private static hcpartyType BuildPerson(string id, string personType, string firstname, string lastname)
        {
            var hcPartyType = new hcpartyType
            {
                cd = new CDHCPARTY[1]
                {
                    new CDHCPARTY
                    {
                        SV = KmehrConstant.ReferenceVersion.CD_HCPARTY_VERSION,
                        S = CDHCPARTYschemes.CDHCPARTY,
                        Value = personType
                    }
                },
                id = new IDHCPARTY[1]
                {
                    new IDHCPARTY
                    {
                        SV = KmehrConstant.ReferenceVersion.ID_KMEHR_VERSION,
                        S = IDHCPARTYschemes.IDHCPARTY,
                        Value = id
                    }
                }
            };
            var choices = new List<ItemsChoiceType>();
            var items = new List<string>();
            if (!string.IsNullOrWhiteSpace(firstname))
            {
                choices.Add(ItemsChoiceType.firstname);
                items.Add(firstname);
            }

            if (!string.IsNullOrWhiteSpace(lastname))
            {
                choices.Add(ItemsChoiceType.familyname);
                items.Add(lastname);
            }

            hcPartyType.ItemsElementName = choices.ToArray();
            hcPartyType.Items = items.ToArray();
            return hcPartyType;
        }
    }
}
