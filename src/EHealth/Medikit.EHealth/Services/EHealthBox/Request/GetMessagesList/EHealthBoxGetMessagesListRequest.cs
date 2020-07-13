// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Xml.Linq;

namespace Medikit.EHealth.Services.EHealthBox.Request
{
    public class EHealthBoxGetMessagesListRequest
    {
        public EHealthBoxGetMessagesListRequest()
        {
            Source = EHealthBoxSources.INBOX;
            StartIndex = 1;
            EndIndex = 100;
        }

        public EHealthBoxIdType BoxId { get; set; }
        public EHealthBoxSources Source { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.EHEALTHBOX_CONSULTATION + "GetMessagesListRequest",
                new XAttribute("xmlns", Constants.XMLNamespaces.EHEALTHBOX_CONSULTATION));
            if (BoxId != null)
            {
                result.Add(BoxId.Serialize());
            }

            result.Add(new XElement("Source", Enum.GetName(typeof(EHealthBoxSources), Source)));
            result.Add(new XElement("StartIndex", StartIndex));
            result.Add(new XElement("EndIndex", EndIndex));
            return result;
        }
    }
}
