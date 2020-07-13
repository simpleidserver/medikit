// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Xml.Linq;

namespace Medikit.EHealth.Services.EHealthBox.Request
{
    public class EHealthBoxFreeInformationsType
    {
        public IEnumerable<byte> EncryptableFreeText { get; set; }

        public XElement Serialize()
        {
            var result = new XElement("FreeInformations");
            if (EncryptableFreeText != null)
            {
                result.Add(new XElement("EncryptableFreeText", EncryptableFreeText));
            }

            return result;
        }
    }
}
