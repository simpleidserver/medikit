// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.EHealth.Application.KMEHRReference.Queries.Results;

namespace Medikit.Api.EHealth.Application.KMEHRReference.Queries
{
    public class GetKMEHRReferenceByCodeQuery : IRequest<KMEHRReferenceTableResult>
    {
        public GetKMEHRReferenceByCodeQuery(string code, string language = null)
        {
            Code = code;
            Language = language;
        }

        public string Code { get; set; }
        public string Language { get; set; }
    }
}
