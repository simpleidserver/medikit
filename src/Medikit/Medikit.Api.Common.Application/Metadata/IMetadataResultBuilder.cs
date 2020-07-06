// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Domains;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.Common.Application.Metadata
{
    public interface IMetadataResultBuilder
    {
        MetadataResultBuilder AddTranslatedEnum<T>(string name);
        MetadataResultBuilder AddTranslatedDomainObject(string name, TranslatedDomainObject translatedDomainObject);
        Task<MetadataResult> Build(string language, CancellationToken cancellationToken);
    }
}
