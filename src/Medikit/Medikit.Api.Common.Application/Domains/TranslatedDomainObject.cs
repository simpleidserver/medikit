// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace Medikit.Api.Common.Application.Domains
{
    public class TranslatedDomainObject
    {
        public ICollection<Translation> Translations { get; set; }
    }
}
