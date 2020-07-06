// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.EHealth.Application.Domains;
using Medikit.Api.EHealth.Application.Persistence;
using Medikit.Api.EHealth.Application.Persistence.InMemory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Medikit.Api.Common.Application
{
    public static class MedikitServerBuilderExtensions
    {
        public static MedikitServerBuilder ImportTableReference(this MedikitServerBuilder builder, string path)
        {
            var referenceTables = new ConcurrentBag<KMEHRReferenceTable>();
            var files = Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories);
            foreach(var file in files)
            {
                referenceTables.Add(Extract(file));
            }

            builder.Services.AddSingleton<IKMEHRReferenceTableQueryRepository>(new InMemoryKMEHRReferenceTableQueryRepository(referenceTables));
            return builder;
        }

        private static KMEHRReferenceTable Extract(string filePath)
        {
            var code = Path.GetFileNameWithoutExtension(filePath);
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var elt = xmlDoc.SelectSingleNode("kmehr-cd");
            var version = elt.SelectSingleNode("VERSION").InnerText;
            var name = elt.SelectSingleNode("NAME").InnerText;
            var publishedDate = DateTime.Parse(elt.SelectSingleNode("DATE").InnerText);
            var status = elt.SelectSingleNode("STATUS").InnerText;
            var records = new List<KMEHRReferenceRecord>();
            foreach (XmlNode valueNode in elt.SelectNodes("VALUE"))
            {
                var translations = new List<KMEHRReferenceRecordTranslation>();
                foreach(XmlNode descriptionNode in valueNode.SelectNodes("DESCRIPTION"))
                {
                    translations.Add(new KMEHRReferenceRecordTranslation
                    {
                        Language = descriptionNode.Attributes["L"].Value,
                        Value = descriptionNode.InnerText
                    });
                }

                records.Add(new KMEHRReferenceRecord
                {
                    Code = valueNode.SelectSingleNode("CODE").InnerText,
                    Translations = translations
                });
            }

            return new KMEHRReferenceTable
            {
                Code = code.ToUpperInvariant(),
                Version = version,
                Name = name,
                PublishedDateTime = publishedDate,
                Status = status,
                Content = records
            };
        }
    }
}
