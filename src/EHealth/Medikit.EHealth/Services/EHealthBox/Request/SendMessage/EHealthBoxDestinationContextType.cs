// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;

namespace Medikit.EHealth.Services.EHealthBox.Request
{
    public class EHealthBoxDestinationContextType
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Quality { get; set; }

        public XElement Serialize()
        {
            var result = new XElement("DestinationContext",
                new XElement("Id", Id),
                new XElement("Type", Type));
            if (!string.IsNullOrWhiteSpace(SubType))
            {
                result.Add(new XElement("SubType", SubType));
            }

            result.Add(new XElement("Quality", Quality));
            return result;
        }
    }
}
