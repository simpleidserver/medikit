// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    public class SOAPTransforms
    {
        [XmlElement(ElementName = "Transform", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public List<SOAPTransform> Transforms { get; set; }
        [XmlElement(ElementName = "DigestMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SOAPDigestMethod DigestMethod { get; set; }
        [XmlElement(ElementName = "DigestValue")]
        public string DigestValue { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.DS + "Transforms");
            if (Transforms != null)
            {
                foreach(var transform in Transforms)
                {
                    result.Add(transform.Serialize());
                }
            }

            if (DigestMethod != null)
            {
                result.Add(DigestMethod.Serialize());
            }

            if (!string.IsNullOrWhiteSpace(DigestValue))
            {
                result.Add(new XElement(Constants.XMLNamespaces.DS + "DigestValue", DigestValue));
            }

            return result;
        }
    }
}
