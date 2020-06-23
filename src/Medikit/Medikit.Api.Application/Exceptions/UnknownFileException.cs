// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.Api.Application.Exceptions
{
    public class UnknownFileException : Exception
    {
        public UnknownFileException(string fileId)
        {
            FileId = fileId;
        }

        public string FileId { get; set; }
    }
}
