// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Xml.Linq;

namespace Medikit.EHealth.Services.EHealthBox.Request
{
    public class EHealthBoxPublicationAnnexType
    {
        public IEnumerable<byte> EncryptableTitle { get; set; }
        public IEnumerable<byte> EncryptableTextContent { get; set; }
        public IEnumerable<byte> EncryptableBinaryContent { get; set; }
        public string DownloadFileName { get; set; }
        public string MimeType { get; set; }
        public string Digest { get; set; }

        public XElement Serialize()
        {
            var result = new XElement("Annex",
                new XElement("EncryptableTitle", EncryptableTitle));
            if (EncryptableTextContent != null)
            {
                result.Add(new XElement("EncryptableTextContent", EncryptableTextContent));
            }

            if (EncryptableBinaryContent != null)
            {
                result.Add(new XElement("EncryptableTextContent", EncryptableTextContent));
            }

            result.Add(new XElement("DownloadFileName", DownloadFileName));
            result.Add(new XElement("MimeType", MimeType));
            result.Add(new XElement("Digest", Digest));
            return result;
        }
    }
}
