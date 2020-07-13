// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;

namespace Medikit.EHealth.Services.EHealthBox.Request
{
    public class EHealthBoxContentSpecificationType
    {
        public EHealthBoxContentSpecificationType()
        {
            IsImportant = false;
            IsEncrypted = false;
            PublicationReceipt = false;
            ReceivedReceipt = false;
            ReadReceipt = false;
        }

        /// <summary>
        /// The application sending the message.
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// Content type of the message : DOCUMENT or NEWS.
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        ///  If the message is to be considered as important.
        /// </summary>
        public bool IsImportant { get; set; }
        /// <summary>
        /// If the content has been encrypted.
        /// </summary>
        public bool IsEncrypted { get; set; }
        /// <summary>
        /// Boolean (true or false) that indicates if a publication receipt is requested.
        /// A message is returned to the sender’s eHealthBox when it is stored in the database.
        /// </summary>
        public bool PublicationReceipt { get; set; }
        /// <summary>
        /// Boolean (true or false) that indicates if a received receipt is requested. 
        /// A message is returned to the sender’s eHealthBox when the recipient has viewed the message
        /// </summary>
        public bool ReceivedReceipt { get; set; }
        /// <summary>
        ///  A message is returned to the sender’s eHealthBox when the message is read by the recipient
        /// </summary>
        public bool ReadReceipt { get; set; }

        public XElement Serialize()
        {
            var result = new XElement("ContentSpecification");
            if (!string.IsNullOrWhiteSpace(ApplicationName))
            {
                result.Add(new XElement("ApplicationName", ApplicationName));
            }

            result.Add(new XElement("ContentType", ContentType));
            result.Add(new XElement("IsImportant", IsImportant));
            result.Add(new XElement("IsEncrypted", IsImportant));
            result.Add(new XElement("PublicationReceipt", PublicationReceipt));
            result.Add(new XElement("ReceivedReceipt", ReceivedReceipt));
            result.Add(new XElement("ReadReceipt", ReadReceipt));
            return result;
        }
    }
}
