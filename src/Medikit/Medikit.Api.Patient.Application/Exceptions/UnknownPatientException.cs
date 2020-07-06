// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.Api.Patient.Application.Exceptions
{
    public class UnknownPatientException : Exception
    {
        public UnknownPatientException(string niss, string message) : base(message)
        {
            Niss = niss;
        }

        public string Niss { get; set; }
    }
}