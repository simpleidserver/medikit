// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;

namespace Medikit.EHealth.Services.EHealthBox.Request
{
    public class EHealthBoxContentContextType
    {
        /// <summary>
        /// Contains the message content (a document or a news item).
        /// </summary>
        public EHealthBoxPublicationContentType Content { get; set; }
        public EHealthBoxContentSpecificationType ContentSpecification { get; set; }

        public XElement Serialize()
        {
            var result = new XElement("ContentContext",
                new XElement(Content.Serialize()),
                new XElement(ContentSpecification.Serialize()));
            return result;
        }
    }
}
