// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.EHealthBox;
using Medikit.EHealth.Services.EHealthBox.Request;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Medikit.EHealth.Console
{
    public partial class Program
    {
        public static async Task GetBoxInfo()
        {
            var boxService = (IEHealthBoxService)_serviceProvider.GetService(typeof(IEHealthBoxService));
            await boxService.GetBoxInfo(new EHealthBoxGetInfoRequest());
        }

        public static async Task GetMessagesList()
        {
            var boxService = (IEHealthBoxService)_serviceProvider.GetService(typeof(IEHealthBoxService));
            await boxService.GetMessagesList(new EHealthBoxGetMessagesListRequest
            {
                Source = EHealthBoxSources.INBOX
            });
        }

        public static async Task SendMessage()
        {
            var boxService = (IEHealthBoxService)_serviceProvider.GetService(typeof(IEHealthBoxService));
            var payload = Encoding.UTF8.GetBytes("Hello world");
            await boxService.SendMessage(new EHealthBoxSendMessageRequest
            {
                DestinationContextLst = new List<EHealthBoxDestinationContextType>
                {
                    new EHealthBoxDestinationContextType
                    {
                        Id = "123456789",
                        Type = "NIHII",
                        Quality = "HOSPITAL"
                    },
                    new EHealthBoxDestinationContextType
                    {
                        Id = "987654321",
                        Type = "NIHII",
                        Quality = "DOCTOR"
                    }
                },
                ContentContext = new EHealthBoxContentContextType
                {
                    Content = new EHealthBoxPublicationContentType
                    {
                        Document = new EHealthBoxPublicationDocumentType
                        {
                            Title = "DocumentTitle",
                            DownloadFileName = "test.txt",
                            MimeType = "application/octet-stream",
                            Digest = GetHASH(payload),
                            EncryptableTextContent = payload
                        }
                    },
                    ContentSpecification = new EHealthBoxContentSpecificationType
                    {
                        ContentType = "DOCUMENT",
                        IsImportant = true,
                        IsEncrypted = false,
                        PublicationReceipt = true,
                        ReadReceipt = true,
                        ReceivedReceipt = true
                    }
                }
            });
        }



        private static byte[] GetHASH(byte[] payload)
        {
            using (var sha = SHA256.Create())
            {
                var hashResult = sha.ComputeHash(payload);
                return hashResult;
            }
        }
    }
}