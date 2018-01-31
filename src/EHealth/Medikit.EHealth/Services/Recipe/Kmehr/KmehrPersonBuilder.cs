// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using System;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public class KmehrPersonBuilder
    {
        private personType _personType;

        public KmehrPersonBuilder New(string id, string familyName, string[] firstNameLst, DateTime? birthDate = null, CDSEXvalues? sex = null)
        {
            _personType = new personType
            {
                id = new IDPATIENT[1]
                {
                    new IDPATIENT
                    {
                        S = IDPATIENTschemes.IDPATIENT,
                        SV = KmehrConstant.ReferenceVersion.ID_KMEHR_VERSION,
                        Value = id
                    }                    
                },
                firstname = firstNameLst,
                familyname = familyName
            };
            if (birthDate != null)
            {
                _personType.birthdate = new dateType
                {
                    Items = new object[1]
                    {
                        birthDate.Value.ToString("yyyy-mm-dd")
                    },
                    ItemsElementName = new ItemsChoiceType1[1]
                    {
                        ItemsChoiceType1.date
                    }
                };
            }

            if (sex != null)
            {
                _personType.sex = new sexType
                {
                    cd = new CDSEX
                    {
                        SV = KmehrConstant.ReferenceVersion.CD_SEX_VERSION,
                        S = KmehrConstant.ReferenceNames.SEX,
                        Value = sex.Value
                    }
                };
            }

            return this;
        }

        internal personType Build()
        {
            return _personType;
        }
    }
}
