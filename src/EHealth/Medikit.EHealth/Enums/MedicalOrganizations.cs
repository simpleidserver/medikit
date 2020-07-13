// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Medikit.EHealth.Enums
{
    public class MedicalOrganizations : Enumeration
    {
        public static MedicalOrganizations AmbulanceService = new MedicalOrganizations(0, "urn:be:fgov:ehealth:1.0:certificateholder:ambulanceservice:nihiinumber", "Ambulance service", "AMBU_SERVICE", "NIHII");


        public MedicalOrganizations(int value, string code, string description, string quality, string type) : base(value, code, description)
        {
        }

        public string Quality { get; set; }
        public string Type { get; set; }
    }
}
