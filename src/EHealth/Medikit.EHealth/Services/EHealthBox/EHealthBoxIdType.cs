// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;

namespace Medikit.EHealth.Services.EHealthBox
{
    public class EHealthBoxIdType
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Quality { get; set; }

        public XElement Serialize()
        {
            var result = new XElement("BoxId",
                new XElement("Id", Id),
                new XElement("Type", Type),
                new XElement("Quality", Quality));
            if (!string.IsNullOrWhiteSpace(SubType))
            {
                result.Add("SubType", SubType);
            }

            return result;
        }
    }
}
