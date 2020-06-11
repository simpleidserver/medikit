// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using System;
using System.Collections.Generic;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrTransactionHeadingBuilder
    {
        private headingType _obj;

        public headingType Build()
        {
            return _obj;
        }

        public KmehrTransactionHeadingBuilder AddMedicationTransactionItem(Action<KmehrTransactionItemBuilder> callback)
        {
            var itemType = new itemType
            {
                id = new IDKMEHR[1]
                {
                    new IDKMEHR
                    {
                        S = IDKMEHRschemes.IDKMEHR,
                        SV = KmehrConstant.ReferenceVersion.ID_KMEHR_VERSION,
                        Value = "1"
                    }
                },
                cd = new CDITEM[1]
                {
                    new CDITEM
                    {
                        S = CDITEMschemes.CDITEM,
                        SV = KmehrConstant.ReferenceVersion.CD_TRANSACTION_MEDICATION_VERSION,
                        Value = KmehrConstant.TransactionItemNames.MEDICATION
                    }
                }
            };
            var builder = new KmehrTransactionItemBuilder(itemType);
            callback(builder);
            var objs = new List<object>();
            if (_obj.Items != null)
            {
                objs.AddRange(_obj.Items);
            }

            objs.Add(builder.Build());
            _obj.Items = objs.ToArray();
            return this;
        }
    }
}
