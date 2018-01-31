// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.DICS.Response
{
    public class DICSFindAmpResponse
    {
        [XmlElement(ElementName = "Amp", Namespace = "", Form = XmlSchemaForm.None)]
        public List<DICSAmp> Amp { get; set; }
    }
}
