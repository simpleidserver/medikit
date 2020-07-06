// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.Api.Medicalfile.Application.Exceptions
{
    public class UnknownMedicalfileException : Exception
    {
        public UnknownMedicalfileException(string medicalFileId, string message) : base(message)
        {
            MedicalFileId = medicalFileId;
        }

        public string MedicalFileId { get; set; }
    }
}
