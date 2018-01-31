// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.Api.Application.Exceptions
{
    public class UnknownPrescriptionException : Exception
    {
        public UnknownPrescriptionException(string prescriptionId, string message) : base(message)
        {
            PrescriptionId = prescriptionId;
        }

        public string PrescriptionId { get; set; }
    }
}
