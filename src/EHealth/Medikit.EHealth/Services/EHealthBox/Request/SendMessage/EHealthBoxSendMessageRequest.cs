// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Xml.Linq;

namespace Medikit.EHealth.Services.EHealthBox.Request
{
    public class EHealthBoxSendMessageRequest
    {
        public EHealthBoxSendMessageRequest()
        {
            DestinationContextLst = new List<EHealthBoxDestinationContextType>();
            CopyMailToLst = new List<string>();
        }

        public EHealthBoxIdType BoxId { get; set; }
        /// <summary>
        /// Contains information about the recipients.
        /// </summary>
        public ICollection<EHealthBoxDestinationContextType> DestinationContextLst { get; set; }
        /// <summary>
        /// Contains the message content and message details.
        /// </summary>
        public EHealthBoxContentContextType ContentContext { get; set; }
        // Meta
        public ICollection<string> CopyMailToLst { get; set; }
        public string PublicationId { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.EHEALTHBOX_PUBLICATION + "SendMessageRequest",
                new XAttribute("xmlns", Constants.XMLNamespaces.EHEALTHBOX_PUBLICATION));
            if (BoxId != null)
            {
                result.Add(BoxId.Serialize());
            }

            foreach(var contextType in DestinationContextLst)
            {
                result.Add(contextType.Serialize());
            }

            result.Add(ContentContext.Serialize());
            foreach(var copyMailTo in CopyMailToLst)
            {
                result.Add(new XElement("CopyMailTo", copyMailTo));
            }

            if (!string.IsNullOrWhiteSpace(PublicationId))
            {
                result.Add(new XAttribute("PublicationId", PublicationId));
            }

            return result;
        }
    }
}
