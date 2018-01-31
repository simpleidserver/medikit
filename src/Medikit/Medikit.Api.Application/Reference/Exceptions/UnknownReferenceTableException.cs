// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;

namespace Medikit.Api.Application.Reference.Exceptions
{
    public class UnknownReferenceTableException : Exception
    {
        public UnknownReferenceTableException(string code)
        {
            Code = code;
        }

        public string Code { get; set; }
    }
}
