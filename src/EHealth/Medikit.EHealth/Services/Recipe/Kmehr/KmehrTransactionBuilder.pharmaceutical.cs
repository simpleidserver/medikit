// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrTransactionBuilder
    {
        public KmehrTransactionBuilder NewPharmaceuticalPrescriptionTransaction(string id, DateTime currentDateTime, bool isComplete = false, bool isValidated = false, DateTime? expirationDate = null)
        {
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

        public KmehrTransactionBuilder AddAuthor(string id, string type, string firstname, string lastname)
        {
            List<hcpartyType> authors;
            if (_transactionType.author == null)
            {
                authors = new List<hcpartyType>();
            }
            else
            {
                authors = _transactionType.author.ToList();
            }

            var hcParty = new hcpartyType
            {
                cd = new CDHCPARTY[1]
                {
                    new CDHCPARTY
                    {
                        SV = KmehrConstant.ReferenceVersion.CD_HCPARTY_VERSION,
                        S = CDHCPARTYschemes.CDHCPARTY,
                        Value = type
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

            hcParty.ItemsElementName = choices.ToArray();
            hcParty.Items = items.ToArray();
            authors.Add(hcParty);
            _transactionType.author = authors.ToArray();
            return this;
        }

        public KmehrTransactionBuilder AddMedicationTransactionItem(Action<KmehrTransactionItemBuilder> callback)
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
