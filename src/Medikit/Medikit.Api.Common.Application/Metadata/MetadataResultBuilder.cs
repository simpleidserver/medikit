// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Common.Application.Metadata
{
    public class MetadataResultBuilder : IMetadataResultBuilder
    {
        private Dictionary<string, Type> _dic;
        private Dictionary<string, TranslatedDomainObject> _translatedDomainObjects;
        private readonly ITranslationService _translationService;

        public MetadataResultBuilder(ITranslationService translationService)
        {
            _dic = new Dictionary<string, Type>();
            _translatedDomainObjects = new Dictionary<string, TranslatedDomainObject>();
            _translationService = translationService;
        }

        public MetadataResultBuilder AddTranslatedEnum<T>(string name)
        {
            _dic.Add(name, typeof(T));
            return this;
        }

        public MetadataResultBuilder AddTranslatedDomainObject(string name, TranslatedDomainObject translatedDomainObject)
        {
            _translatedDomainObjects.Add(name, translatedDomainObject);
            return this;
        }

        public async Task<MetadataResult> Build(string language, CancellationToken cancellationToken)
        {
            var languages = await _translationService.GetLanguages(cancellationToken);
            bool languageExists = languages.Any(_ => _.Code == language);
            var translationCodes = new List<string>();
            var result = new MetadataResult();
            if(_dic.Any())
            {
                foreach(var kvp in _dic)
                {
                    translationCodes.AddRange(GetTranslationCodes(kvp.Value));
                }

                ICollection<Translation> translations;
                if (languageExists)
                {
                    translations = await _translationService.GetTranslations(translationCodes, language, cancellationToken);
                }
                else
                {
                    translations = await _translationService.GetTranslations(translationCodes, cancellationToken);
                }

                foreach(var kvp in _dic)
                {
                    result.Content.Add(kvp.Key, BuildMetadataRecord(kvp.Value, language, translations));
                }
            }

            if (_translatedDomainObjects.Any())
            {
                foreach(var kvp in _translatedDomainObjects)
                {
                    result.Content.Add(kvp.Key, BuildMetadataRecord(language, kvp.Key, kvp.Value.Translations, languages));
                }
            }

            return result;
        }

        public MetadataRecord BuildMetadataRecord(string defaultLanguage, string defaultValue, ICollection<Translation> translations, ICollection<Language> languages)
        {
            var result = new MetadataRecord();
            var languageExists = languages.Any(_ => _.Code == defaultLanguage);
            if(languageExists)
            {
                var translation = translations.FirstOrDefault(_ => _.LanguageCode == defaultLanguage);
                result.Translations.Add(new TranslationResult
                {
                    Language = defaultLanguage,
                    Value = translation == null ? $"[{defaultValue}]" : translation.Value
                });
                return result;
            }

            foreach(var language in languages)
            {
                var translation = translations.FirstOrDefault(_ => _.LanguageCode == defaultLanguage);
                result.Translations.Add(new TranslationResult
                {
                    Language = defaultLanguage,
                    Value = translation == null ? $"[{defaultValue}]" : translation.Value
                });
            }

            return result;
        }

        private MetadataRecord BuildMetadataRecord(Type type, string defaultLanguage, ICollection<Translation> translations)
        {
            var names = Enum.GetNames(type);
            var result = new MetadataRecord();
            foreach(var name in names)
            {
                var child = new MetadataRecord();
                var value = GetValue(type, name);
                var trs = translations.Where(_ => _.Code == GetTranslationCode(type, name));
                if (!trs.Any())
                {
                    child.Translations.Add(new TranslationResult
                    {
                        Language = defaultLanguage,
                        Value = $"[{name}]"
                    });
                }
                else
                {
                    foreach(var tr in trs)
                    {
                        child.Translations.Add(new TranslationResult
                        {
                            Language = tr.LanguageCode,
                            Value = tr.Value
                        });
                    }
                }

                result.Children.Add(value, child);
            }

            return result;
        }

        private static ICollection<string> GetTranslationCodes(Type type)
        {
            var names = Enum.GetNames(type);
            return names.Select(_ => GetTranslationCode(type, _)).ToList();
        }

        private static string GetTranslationCode(Type type, string name)
        {
            return $"{type.Name}_{name}";
        }

        private static string GetValue(Type type, string name)
        {
            return ((int)Enum.Parse(type, name)).ToString();
        }
    }
}
