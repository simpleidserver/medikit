// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;

namespace Medikit.Api.Application.Exceptions
{
    public class BadAssertionTokenException : Exception
    {
        public BadAssertionTokenException(string message) : base(message)
        {
        }
    }
}
