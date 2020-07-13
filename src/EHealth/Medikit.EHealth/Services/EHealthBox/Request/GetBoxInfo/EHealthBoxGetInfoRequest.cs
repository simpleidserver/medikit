// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;

namespace Medikit.EHealth.Services.EHealthBox.Request
{
    public class EHealthBoxGetInfoRequest
    {
        public EHealthBoxIdType BoxId { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.EHEALTHBOX_CONSULTATION + "GetBoxInfoRequest",
                new XAttribute("xmlns", Constants.XMLNamespaces.EHEALTHBOX_CONSULTATION));
            if (BoxId != null)
            {
                result.Add(BoxId.Serialize());
            }

            return result;
        }
    }
}
