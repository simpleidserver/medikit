// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.IO;
using System.Text;
using System.Xml;

namespace Medikit.EHealth.Xml
{
    public class FullElementXmlTextWriter : XmlTextWriter
    {
        public FullElementXmlTextWriter(Stream stream) : base(stream, Encoding.UTF8)
        {
            
        }

        public override void WriteEndElement()
        {
            base.WriteFullEndElement();
        }
    }
}
