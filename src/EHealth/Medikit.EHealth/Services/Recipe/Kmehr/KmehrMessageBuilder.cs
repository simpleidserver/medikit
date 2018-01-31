// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public class KmehrMessageBuilder
    {
        private List<hcpartyType> _senders;
        private List<hcpartyType> _recipients;
        private List<folderType> _folders;

        public KmehrMessageBuilder()
        {
            _senders = new List<hcpartyType>();
            _recipients = new List<hcpartyType>();
            _folders = new List<folderType>();
        }

        public KmehrMessageBuilder AddFolder(string id, Action<KmehrPersonBuilder> patientCallback = null, Action<KmehrTransactionLstBuilder> transactionLstCallback = null)
        {
            var folder = new folderType
            {
                id = new IDKMEHR[1]
                {
                    new IDKMEHR
                    {
                        SV = KmehrConstant.ReferenceVersion.ID_KMEHR_VERSION,
                        S = IDKMEHRschemes.IDKMEHR,
                        Value = id
                    }
                }
            };
            if (patientCallback != null)
            {
                var builder = new KmehrPersonBuilder();
                patientCallback(builder);
                folder.patient = builder.Build();
            }

            if (transactionLstCallback != null)
            {
                var builder = new KmehrTransactionLstBuilder();
                transactionLstCallback(builder);
                folder.transaction = builder.TransactionLst.ToArray();
            }

            _folders.Add(folder);
            return this;
        }

        public KmehrMessageBuilder AddSender(Action<KmehrHealthCarePartyLstBuilder> callback)
        {
            var builder = new KmehrHealthCarePartyLstBuilder();
            callback(builder);
            _senders.AddRange(builder.HcParties);
            return this;
        }

        public KmehrMessageBuilder AddRecipient(Action<KmehrHealthCarePartyLstBuilder> callback)
        {
            var builder = new KmehrHealthCarePartyLstBuilder();
            callback(builder);
            _recipients.AddRange(builder.HcParties);
            return this;
        }

        public kmehrmessageType Build()
        {
            var internalIdentifier = Guid.NewGuid().ToString();
            var firstPartyIdentifier = _senders.First().id.First().Value;
            var currentDateTime = DateTime.UtcNow;
            var items = new List<object>();
            items.AddRange(_folders);
            var message = new kmehrmessageType
            {
                header = new headerType
                {
                    standard = new standardType
                    {
                        cd = new CDSTANDARD
                        {
                            SV = KmehrConstant.ReferenceVersion.CD_STANDARD_VERSION, // Specifies the version of the reference table.
                            Value = CDSTANDARDvalues.Item20200301 // This is the latest version of the Belgian Healthcare Telematics Standard to which the message complies.
                        }
                    },
                    id = new IDKMEHR[1]
                    {
                        new IDKMEHR { S = IDKMEHRschemes.IDKMEHR, Value = $"{firstPartyIdentifier}.{internalIdentifier}", SV = KmehrConstant.ReferenceVersion.ID_KMEHR_VERSION } // // the first ID-HCPARTY + "." + local unique identifier.
                    },
                    date = currentDateTime.ToString("yyyy:mm:dd"), // Date of the creation of the message.
                    time = currentDateTime.ToString("hh:mm:ss"), // Time of the creation of the message.
                    sender = _senders.ToArray(), // Contains a combination of hcparty that specifies the sender of the message.
                    recipient = new List<recipientType>
                    {
                        new recipientType
                        {
                            hcparty = _recipients.ToArray()
                        }
                    }.ToArray() // Contains a combination of hcparty that specifies the receiver of the message.
                },
                Items = items.ToArray()
            };
            return message;
        }

        public string BuildAndSerialize()
        {
            var message = Build();
            var serializer = new XmlSerializer(typeof(kmehrmessageType));
            XmlWriterSettings settings = new XmlWriterSettings();
            byte[] result = null;
            using (var ms = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(ms, settings))
                {
                    serializer.Serialize(writer, message);
                    result = ms.ToArray();
                }
            }

            return Encoding.UTF8.GetString(result);
        }
    }
}
