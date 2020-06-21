// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Medikit.Api.Application.Prescriptions.Commands
{
    public class RevokePrescriptionCommand
    {
        public string AssertionToken { get; set; }
        public string Rid { get; set; }
        public string Reason { get; set; }
    }
}
