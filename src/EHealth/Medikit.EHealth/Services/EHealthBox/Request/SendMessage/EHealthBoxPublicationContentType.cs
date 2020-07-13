// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Xml.Linq;

namespace Medikit.EHealth.Services.EHealthBox.Request
{
    public class EHealthBoxPublicationContentType
    {
        public EHealthBoxPublicationContentType()
        {
            AnnexLst = new List<EHealthBoxPublicationAnnexType>();
        }

        public EHealthBoxPublicationDocumentType Document { get; set; }
        public EHealthBoxFreeInformationsType FreeInformations { get; set; }
        public IEnumerable<byte> EncryptableINSSPatient { get; set; }
        public ICollection<EHealthBoxPublicationAnnexType> AnnexLst { get; set; }

        public XElement Serialize()
        {
            var result = new XElement("Content",
                Document.Serialize());
            if (FreeInformations != null)
            {
                result.Add(FreeInformations.Serialize());
            }

            if (EncryptableINSSPatient != null)
            {
                result.Add("EncryptableINSSPatient", EncryptableINSSPatient);
            }

            foreach(var annex in AnnexLst)
            {
                result.Add(annex.Serialize());
            }

            return result;
        }
    }
}
