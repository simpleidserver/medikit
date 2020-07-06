// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Medikit.Api.Common.Application.Domains
{
    public class Language : TranslatedDomainObject, ICloneable
    {
        public Language()
        {
            Translations = new List<Translation>();
        }

        public string Code { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }

        public object Clone()
        {
            return new Language
            {
                Code = Code,
                CreateDateTime = CreateDateTime,
                UpdateDateTime = UpdateDateTime,
                Translations = Translations.Select(_ => (Translation)_.Clone()).ToList()
            };
        }
    }
}
