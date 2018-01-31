// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe.Kmehr.Enums;
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using System.Linq;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrHealthCarePartyBuilder
    {
        private hcpartyType _hcParty;

        public KmehrHealthCarePartyBuilder AddAddress(KmehrAddressTypes addressType, string country, string zipCode = null, string city = null, string street = null, string houseNumber = null, string postboxNumber = null)
        {
            var newAddressType = new addressType
            {
                cd = new CDADDRESS[1]
                {
                    new CDADDRESS
                    {
                        S = CDADDRESSschemes.CDADDRESS,
                        SV = KmehrConstant.ReferenceVersion.CD_ADDRESS_VERSION,
                        Value = addressType.Code
                    }
                },
                country = new countryType
                {
                    cd = new CDCOUNTRY
                    {
                        S = CDCOUNTRYschemes.CDFEDCOUNTRY,
                        SV = KmehrConstant.ReferenceVersion.CD_FEDICT_COUNTRY_CODE_VERSION,
                        Value = country
                    }
                },
                zip = zipCode,
                city = city,
                street = street,
                housenumber = houseNumber,
                postboxnumber = postboxNumber

            };
            if (_hcParty.address == null)
            {
                _hcParty.address = new addressType[0];
            }

            var addresses = _hcParty.address.ToList();
            addresses.Add(newAddressType);
            _hcParty.address = addresses.ToArray();
            return this;
        }

        public KmehrHealthCarePartyBuilder AddTelecom(KmehrTelecomTypes telecomType, string value)
        {
            var newTelecomType = new telecomType
            {
                cd = new CDTELECOM[1]
                {
                    new CDTELECOM
                    {
                        SV = KmehrConstant.ReferenceVersion.CD_TELECOM_TYPE_VERSION,
                        S = CDTELECOMschemes.CDTELECOM,
                        Value = telecomType.Code
                    }
                },
                telecomnumber = value
            };
            if (_hcParty.telecom == null)
            {
                _hcParty.telecom = new telecomType[0];
            }
                       
            var telecoms = _hcParty.telecom.ToList();
            telecoms.Add(newTelecomType);
            _hcParty.telecom = telecoms.ToArray();
            return this;
        }

        internal hcpartyType Build()
        {
            return _hcParty;
        }
    }
}
