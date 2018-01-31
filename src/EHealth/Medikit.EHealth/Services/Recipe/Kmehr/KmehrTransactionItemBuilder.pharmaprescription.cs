// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using System.Collections.Generic;
using System.Linq;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrTransactionItemBuilder
    {
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

        public KmehrTransactionItemBuilder SetInstructionForReimbursement(string instruction, string language)
        {
            _obj.instructionforreimbursement = new textType
            {
                L = language,
                Value = instruction
            };
            return this;
        }

        public KmehrTransactionItemBuilder SetMedicinalProduct(string version, string cnk, string name)
        {
            var content = new contentType
            {
                Items = new object[1]
                {
                    new medicinalProductType
                    {
                        deliveredcd = new CDDRUGCNK[1]
                        {
                            new CDDRUGCNK
                            {
                                SV = version,
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
