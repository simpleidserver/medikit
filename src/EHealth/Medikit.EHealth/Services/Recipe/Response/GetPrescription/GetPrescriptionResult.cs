// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using System;

namespace Medikit.EHealth.Services.Recipe.Response
{
    public class GetPrescriptionResult
    {
        public int Status { get; set; }
        public kmehrmessageType KmehrmessageType { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Rid { get; set; }
        public bool FeedbackAllowed { get; set; }
        public string PatientId { get; set; }
    }
}
