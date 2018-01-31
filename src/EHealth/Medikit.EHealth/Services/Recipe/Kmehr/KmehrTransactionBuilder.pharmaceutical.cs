// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using System;
using System.Collections.Generic;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrTransactionBuilder
    {
        public KmehrTransactionBuilder NewPharmaceuticalPrescriptionTransaction(string id, bool isComplete = false, bool isValidated = false, DateTime? expirationDate = null)
        {
            var currentDateTime = DateTime.UtcNow;
            var transaction = new transactionType
            {
                cd = new CDTRANSACTION[1]
                {
                    new CDTRANSACTION
                    {
                        S = CDTRANSACTIONschemes.CDTRANSACTION,
                        SV = KmehrConstant.ReferenceVersion.CD_PHARMACEUTICAL_PRESCRIPTION_VERSION,
                        Value = KmehrConstant.TransactionNames.PHARMACEUTICAL_PRESCRIPTION
                    }
                },
                date = currentDateTime.ToString("yyyy:mm:dd"),
                time = currentDateTime.ToString("hh:mm:ss"),
                iscomplete = isComplete,
                isvalidated = isValidated
            };
            if (expirationDate != null)
            {
                transaction.expirationdate = expirationDate.Value;
            }

            if (!string.IsNullOrWhiteSpace(id))
            {
                transaction.id = new IDKMEHR[1]
                {
                    new IDKMEHR
                    {
                        S = IDKMEHRschemes.IDKMEHR,
                        SV = KmehrConstant.ReferenceVersion.ID_KMEHR_VERSION,
                        Value = id
                    }
                };
            }

            _transactionType = transaction;
            return this;
        }

        public KmehrTransactionBuilder AddTransactionItem(Action<KmehrTransactionItemBuilder> callback)
        {
            var itemType = new itemType();
            var builder = new KmehrTransactionItemBuilder(itemType);
            callback(builder);
            var objs = new List<object>();
            if (_transactionType.Items != null)
            {
                objs.AddRange(_transactionType.Items);
            }

            objs.Add(builder.Build());
            _transactionType.Items = objs.ToArray();
            return this;
        }

        public KmehrTransactionBuilder AddTransactionHeading(Action<KmehrTransactionHeadingBuilder> callback)
        {
            var builder = new KmehrTransactionHeadingBuilder();
            callback(builder);
            var objs = new List<object>();
            if (_transactionType.Items != null)
            {
                objs.AddRange(_transactionType.Items);
            }

            objs.Add(builder.Build());
            _transactionType.Items = objs.ToArray();
            return this;
        }
    }
}
