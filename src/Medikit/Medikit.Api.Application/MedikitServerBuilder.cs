// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;
using Medikit.Api.Application.Persistence;
using Medikit.Api.Application.Persistence.InMemory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Medikit.Api.Application
{
    public class MedikitServerBuilder
    {
        private IServiceCollection _services;

        public MedikitServerBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public MedikitServerBuilder ImportTableReference(string path)
        {
            var referenceTables = new ConcurrentBag<ReferenceTable>();
            var files = Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories);
            foreach(var file in files)
            {
                referenceTables.Add(Extract(file));
            }

            _services.AddSingleton<IReferenceTableQueryRepository>(new InMemoryReferenceTableQueryRepository(referenceTables));
            return this;
        }

        public MedikitServerBuilder AddLanguages(ICollection<string> lst)
        {
            var languages = new ConcurrentBag<Language>();
            foreach(var record in lst)
            {
                languages.Add(new Language
                {
                    Code = record,
                    CreateDateTime = DateTime.UtcNow,
                    UpdateDateTime = DateTime.UtcNow
                });
            }

            _services.AddSingleton<ILanguageQueryRepository>(new InMemoryLanguageQueryRepository(languages));
            return this;
        }

        public MedikitServerBuilder AddPatients(ConcurrentBag<PatientAggregate> patients)
        {
            _services.AddSingleton<IPatientCommandRepository>(new InMemoryPatientCommandRepository(patients));
            _services.AddSingleton<IPatientQueryRepository>(new InMemoryPatientQueryRepository(patients));
            return this;
        }

        private static ReferenceTable Extract(string filePath)
        {
            var code = Path.GetFileNameWithoutExtension(filePath);
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var elt = xmlDoc.SelectSingleNode("kmehr-cd");
            var version = elt.SelectSingleNode("VERSION").InnerText;
            var name = elt.SelectSingleNode("NAME").InnerText;
            var publishedDate = DateTime.Parse(elt.SelectSingleNode("DATE").InnerText);
            var status = elt.SelectSingleNode("STATUS").InnerText;
            var records = new List<ReferenceRecord>();
            foreach (XmlNode valueNode in elt.SelectNodes("VALUE"))
            {
                var translations = new List<ReferenceRecordTranslation>();
                foreach(XmlNode descriptionNode in valueNode.SelectNodes("DESCRIPTION"))
                {
                    translations.Add(new ReferenceRecordTranslation
                    {
                        Language = descriptionNode.Attributes["L"].Value,
                        Value = descriptionNode.InnerText
                    });
                }

                records.Add(new ReferenceRecord
                {
                    Code = valueNode.SelectSingleNode("CODE").InnerText,
                    Translations = translations
                });
            }

            return new ReferenceTable
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
