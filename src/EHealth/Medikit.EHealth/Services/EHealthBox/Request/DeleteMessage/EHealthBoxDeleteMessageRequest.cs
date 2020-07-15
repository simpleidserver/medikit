// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Medikit.EHealth.Services.EHealthBox.Request
{
    public class EHealthBoxDeleteMessageRequest
    {
        public EHealthBoxDeleteMessageRequest()
        {
            MessageIdLst = new List<string>();
        }

        public EHealthBoxIdType BoxId { get; set; }
        public EHealthBoxSources Source { get; set; }
        public List<string> MessageIdLst { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.EHEALTHBOX_CONSULTATION + "DeleteMessageRequest",
                new XAttribute("xmlns", Constants.XMLNamespaces.EHEALTHBOX_CONSULTATION));
            if (BoxId != null)
            {
                result.Add(BoxId.Serialize());
            }

            result.Add(new XElement("Source", Enum.GetName(typeof(EHealthBoxSources), Source)));
            foreach(var messageId in MessageIdLst)
            {
                result.Add(new XElement("MessageId", messageId));
            }

            return result;
        }
    }
}
