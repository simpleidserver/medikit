// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Medikit.EHealth.Services.EHealthBox.Request
{
    public class EHealthBoxPublicationDocumentType
    {
        /// <summary>
        /// A document has a title, a human readable description of its content.
        /// </summary>
        public string Title { get; set; }
        public IEnumerable<byte> EncryptableTextContent { get; set; }
        /// <summary>
        /// If IsEncrypted is true, the attachment must be encrypted.
        /// </summary>
        public IEnumerable<byte> EncryptableBinaryContent { get; set; }
        /// <summary>
        /// Download file name for example "principal.pdf".
        /// </summary>
        public string DownloadFileName { get; set; }
        /// <summary>
        /// Represents the mime type of the content : application/pdf, text/plain, application/octet-stream.
        /// </summary>
        public string MimeType { get; set; }
        /// <summary>
        /// The SHA-256 hash of the content after encryption.
        /// </summary>
        public IEnumerable<byte> Digest { get; set; }

        public XElement Serialize()
        {
            var result = new XElement("Document", new XElement("Title", Title));
            if (EncryptableTextContent != null)
            {
                result.Add(new XElement("EncryptableTextContent", Convert.ToBase64String(EncryptableTextContent.ToArray())));
            }

            if (EncryptableBinaryContent != null)
            {
                result.Add(new XElement("EncryptableBinaryContent", Convert.ToBase64String(EncryptableBinaryContent.ToArray())));
            }

            result.Add(new XElement("DownloadFileName", DownloadFileName));
            result.Add(new XElement("MimeType", MimeType));
            result.Add(new XElement("Digest", Convert.ToBase64String(Digest.ToArray())));
            return result;
        }
    }
}
