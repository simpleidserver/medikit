// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Enums;
using Medikit.EHealth.ETK;
using System;

namespace Medikit.EHealth
{
    public class EHealthOptions
    {
        public EHealthOptions()
        {
            ProductName = "Medikit";
            Version = "1.0.0";
            Timeout = TimeSpan.FromSeconds(5);
            EtkUrl = "https://services-acpt.ehealth.fgov.be/EtkDepot/v1";
            KgssUrl = "https://services-acpt.ehealth.fgov.be/Kgss/v1";
            DicsUrl = "https://services-acpt.ehealth.fgov.be/Dics/v5";
            StsUrl = "https://services-acpt.ehealth.fgov.be/IAM/Saml11TokenService/v1";
            PrescriberUrl = "https://services-acpt.ehealth.fgov.be/Recip-e/v4/Prescriber";
            CivicsUrl = "https://services-acpt.ehealth.fgov.be/ChapIVInformation/Consultation/v2";
            EHealthboxConsultation = "https://services-acpt.ehealth.fgov.be/ehBoxConsultation/v3";
            EhealthboxPublication = "https://services-acpt.ehealth.fgov.be/ehBoxPublication/v3";
            OrgType = ETKTypes.CBE;
            IdentityProfession = MedicalProfessions.Doctor;
        }

        public string ProductName { get; set; }
        public string Version { get; set; }
        public TimeSpan Timeout { get; set; }
        public string EtkUrl { get; set; }
        public string KgssUrl { get; set; }
        public string DicsUrl { get; set; }
        public string StsUrl { get; set; }
        public string PrescriberUrl { get; set; }
        public string CivicsUrl { get; set; }
        public string EHealthboxConsultation { get; set; }
        public string EhealthboxPublication { get; set; }
        public string OrgCertificateStore{ get; set; }
        public string OrgCertificateStorePassword { get; set; }
        public ETKTypes OrgType { get; set; }
        public string IdentityCertificateStore { get; set; }
        public string IdentityCertificateStorePassword { get; set; }
        public MedicalProfessions IdentityProfession { get; set; }
    }
}
