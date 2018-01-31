// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe
{
    public class SecuredContentType
    {
        [XmlElement(ElementName = "SecuredContent", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SecuredContent { get; set; }

        public XElement Serialize(string name)
        {
            var result = new XElement(name,
                new XElement("SecuredContent", SecuredContent));
            return result;
        }
    }
}
