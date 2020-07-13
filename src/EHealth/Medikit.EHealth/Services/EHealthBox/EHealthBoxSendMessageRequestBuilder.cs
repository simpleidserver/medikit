// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Enums;
using Medikit.EHealth.Services.EHealthBox.Request;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Medikit.EHealth.Services.EHealthBox
{
    public class EHealthBoxSendMessageRequestBuilder
    {
        private ICollection<EHealthBoxDestinationContextType> _destinationContextLst;
        private EHealthBoxContentContextType _content;

        private EHealthBoxSendMessageRequestBuilder()
        {
            _destinationContextLst = new List<EHealthBoxDestinationContextType>();
            _content = new EHealthBoxContentContextType();
        }

        public static EHealthBoxSendMessageRequestBuilder New()
        {
            return new EHealthBoxSendMessageRequestBuilder();
        }

        public EHealthBoxSendMessageRequestBuilder AddDestination(MedicalProfessions profession, string value)
        {
            _destinationContextLst.Add(new EHealthBoxDestinationContextType
            {
                Id = value,
                Quality = profession.Quality,
                Type = "NIHII"
            });
            return this;
        }

        public EHealthBoxSendMessageRequestBuilder AddDestination(MedicalOrganizations org, string value)
        {
            _destinationContextLst.Add(new EHealthBoxDestinationContextType
            {
                Id = value,
                Quality = org.Quality,
                Type = org.Type
            });
            return this;
        }

        public EHealthBoxSendMessageRequestBuilder UploadDocument(string title, string path, bool isImportant = false, bool publicationReceipt = false, bool readReceipt = false, bool receivedReceipt = false)
        {
            var payload = File.ReadAllBytes(path);
            var fileName = Path.GetFileName(path);
            var digest = BuildHASH256(payload);
            _content.Content = new EHealthBoxPublicationContentType
            {
                Document = new EHealthBoxPublicationDocumentType
                {
                    Title = title,
                    DownloadFileName = fileName,
                    MimeType = "application/octet-stream",
                    Digest = digest,
                    EncryptableTextContent = payload
                }
            };
            _content.ContentSpecification = new EHealthBoxContentSpecificationType
            {
                ContentType = "DOCUMENT",
                IsImportant = isImportant,
                IsEncrypted = false,
                PublicationReceipt = publicationReceipt,
                ReadReceipt = readReceipt,
                ReceivedReceipt = receivedReceipt
            };
            return this;
        }

        public EHealthBoxSendMessageRequest Build()
        {
            return new EHealthBoxSendMessageRequest
            {
                DestinationContextLst = _destinationContextLst,
                ContentContext = _content
            };
        }

        private static byte[] BuildHASH256(byte[] payload)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashResult = sha256.ComputeHash(payload);
                return hashResult;
            }
        }
    }
}
