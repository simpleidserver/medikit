// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Medikit.EHealth.Enums
{
    public class MedicalProfessions : Enumeration
    {
        public static MedicalProfessions Doctor = new MedicalProfessions(0, "urn:be:fgov:person:ssin:ehealth:1.0:doctor:nihii11", "The NIHII number of the doctor");
        public static MedicalProfessions Nurse = new MedicalProfessions(1, "urn:be:fgov:person:ssin:ehealth:1.0:nihii:nurse:nihii11", "The NIHII number of the nurse");
        public static MedicalProfessions Physiotherapist = new MedicalProfessions(2, "urn:be:fgov:person:ssin:ehealth:1.0:nihii:physiotherapist:nihii11", "The NIHII number of the physiotherapist");
        public static MedicalProfessions Dentist = new MedicalProfessions(3, "urn:be:fgov:person:ssin:ehealth:1.0:nihii:dentist:nihii11", "The NIHII number of the dentist");
        public static MedicalProfessions Dietician = new MedicalProfessions(4, "urn:be:fgov:person:ssin:ehealth:1.0:nihii:dietician:nihii11", "The NIHII number of the midwife");
        public MedicalProfessions(int value, string code, string description) : base(value, code, description) { }
    }
}
