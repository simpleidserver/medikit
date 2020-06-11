// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrTransactionItemBuilder
    {
        public KmehrTransactionItemBuilder AddAuthor(string id, string type, string firstname, string lastname)
        {
            List<hcpartyType> authors;
            if (_obj.author == null)
            {
                authors = new List<hcpartyType>();
            }
            else
            {
                authors = _obj.author.ToList();
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
            _obj.author = authors.ToArray();
            return this;
        }

        public KmehrTransactionItemBuilder SetPosologyFreeText(string text, string language)
        {
            _obj.posology = new itemTypePosology
            {
                ItemsElementName = new[] { ItemsChoiceType3.text },
                Items = new object[]
                {
                    new textType
                    {
                        L = language,
                        Value = text
                    }
                }
            };

            return this;
        }

        public KmehrTransactionItemBuilder SetInstructionForPatient(string instruction, string language)
        {
            _obj.instructionforpatient = new textType
            {
                L = language,
                Value = instruction
            };
            return this;
        }

        public KmehrTransactionItemBuilder SetBeginMoment(DateTime beginMoment)
        {
            _obj.beginmoment = new momentType
            {
                Items = new object[1]
                {
                    beginMoment
                },
                ItemsElementName = new ItemsChoiceType1[1]
                {
                    ItemsChoiceType1.date
                }
            };
            return this;
        }

        public KmehrTransactionItemBuilder SetInstructionForReimbursement(string instruction, string language)
        {
            _obj.instructionforreimbursement = new textType
            {
                L = language,
                Value = instruction
            };
            return this;
        }

        public KmehrTransactionItemBuilder SetMedicinalProduct(string cnk, string name)
        {
            var content = new contentType
            {
                Items = new object[1]
                {
                    new medicinalProductType
                    {
                        intendedcd = new CDDRUGCNK[1]
                        {
                            new CDDRUGCNK
                            {
                                SV = "LOCALDB",
                                S = CDDRUGCNKschemes.CDDRUGCNK,
                                Value = cnk
                            }
                        }
                    }
                },
                ItemsElementName = new ItemsChoiceType2[1]
                {
                    ItemsChoiceType2.medicinalproduct
                }
            };
            var itemType = (itemType)_obj;
            var contentTypeLst = new List<contentType>();
            if (itemType.content != null)
            {
                contentTypeLst.AddRange(itemType.content.ToList());
            }

            contentTypeLst.Add(content);
            itemType.content = contentTypeLst.ToArray();
            return this;
        }
    }
}
