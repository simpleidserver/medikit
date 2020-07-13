// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Medikit.EHealth.Enums
{
    public class MedicalProfessions : Enumeration
    {
        public static MedicalProfessions Doctor = new MedicalProfessions(0, "urn:be:fgov:person:ssin:ehealth:1.0:doctor:nihii11", "The NIHII number of the doctor", "urn:be:fgov:person:ssin:doctor:boolean", "DOCTOR");
        public static MedicalProfessions Nurse = new MedicalProfessions(1, "urn:be:fgov:person:ssin:ehealth:1.0:nihii:nurse:nihii11", "The NIHII number of the nurse", "urn:be:fgov:person:ssin:nurse:boolean", "NURSE");
        public static MedicalProfessions Physiotherapist = new MedicalProfessions(2, "urn:be:fgov:person:ssin:ehealth:1.0:nihii:physiotherapist:nihii11", "The NIHII number of the physiotherapist", "urn:be:fgov:person:ssin:ehealth:1.0:professional:physiotherapist:boolean", "PHYSIOTHERAPIST");
        public static MedicalProfessions Dentist = new MedicalProfessions(3, "urn:be:fgov:person:ssin:ehealth:1.0:nihii:dentist:nihii11", "The NIHII number of the dentist", "urn:be:fgov:person:ssin:ehealth:1.0:professional:dentist:boolean", "DENTIST");
        public static MedicalProfessions Dietician = new MedicalProfessions(4, "urn:be:fgov:person:ssin:ehealth:1.0:nihii:dietician:nihii11", "The NIHII number of the dietician", "urn:be:fgov:person:ssin:ehealth:1.0:professional:dietician:boolean", "DIETICIAN");
        public static MedicalProfessions Logopedist = new MedicalProfessions(5, "urn:be:fgov:person:ssin:ehealth:1.0:nihii:logopedist:nihii11", "The NIHII number of the logopedist", "urn:be:fgov:person:ssin:ehealth:1.0:professional:logopedist:boolean", "LOGOPEDIST");
        public static MedicalProfessions TrussMaker = new MedicalProfessions(6, "urn:be:fgov:person:ssin:ehealth:1.0:nihii:trussmaker:nihii11", "The NIHII number of the truss maker", "urn:be:fgov:person:ssin:ehealth:1.0:professional:trussmaker:boolean", "TRUSS_MAKER");
        public static MedicalProfessions Orthopedist = new MedicalProfessions(7, "urn:be:fgov:person:ssin:ehealth:1.0:nihii:orthopedist:nihii11", "The NIHII number of the orthopedist", "urn:be:fgov:person:ssin:ehealth:1.0:professional:orthopedist:boolean", "ORTHOPEDIST");
        public static MedicalProfessions Midwife = new MedicalProfessions(8, "urn:be:fgov:person:ssin:ehealth:1.0:nihii:midwife:nihii11", "The NIHII number of the midwife", "urn:be:fgov:person:ssin:ehealth:1.0:professional:midwife:boolean", "MIDWIFE");
        public static MedicalProfessions Optician = new MedicalProfessions(9, "urn:be:fgov:person:ssin:ehealth:1.0:nihii:optician:nihii11", "The NIHII number of the optician", "urn:be:fgov:person:ssin:ehealth:1.0:professional:optician:boolean", "OPTICIEN");
        public static MedicalProfessions Podologist = new MedicalProfessions(11, "urn:be:fgov:person:ssin:ehealth:1.0:nihii:podologist:nihii11", "The NIHII number of the podologist", "urn:be:fgov:person:ssin:ehealth:1.0:professional:podologist:boolean", "PODOLOGIST");

        public MedicalProfessions(int value, string code, string description, string certificationAttribute, string quality) : base(value, code, description) 
        {
            CertificationAttribute = certificationAttribute;
            Quality = quality;
        }

        public string CertificationAttribute { get; private set; }
        public string Quality { get; private set; }
    }
}
