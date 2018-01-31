using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrHealthCarePartyBuilder
    {
        public KmehrHealthCarePartyBuilder AddOrganization(string id, string organizationType, string name = null)
        {
            _hcParty = BuildOrganization(id, organizationType, name);
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
    }
}
