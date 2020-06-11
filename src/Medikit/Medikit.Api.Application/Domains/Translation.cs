// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.Api.Application.Domains
{
    public class Translation : ICloneable
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string LanguageCode { get; set; }
        public string Value { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }

        public object Clone()
        {
            return new Translation
            {
                Code = Code,
                CreateDateTime = CreateDateTime,
                Id = Id,
                LanguageCode = LanguageCode,
                UpdateDateTime = UpdateDateTime,
                Value = Value
            };
        }
    }
}
