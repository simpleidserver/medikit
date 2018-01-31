// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml;

namespace Medikit.EHealth.Xml
{
    public class XmlDsigDocument : XmlDocument
    {
        public const string XmlDsigNamespacePrefix = "ds";

        public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                prefix = GetPrefix(namespaceURI);
            }

            return base.CreateElement(prefix, localName, namespaceURI);
        }

        public static XmlNode SetPrefix(string prefix, XmlNode node)
        {
            foreach (XmlNode n in node.ChildNodes)
            {
                SetPrefix(prefix, n);
            }

            if (node.NamespaceURI == "http://www.w3.org/2001/10/xml-exc-c14n#")
                node.Prefix = "ec";
            else if ((node.NamespaceURI == SignedXmlWithId.XmlDsigNamespaceUrl) || (string.IsNullOrEmpty(node.Prefix)))
                node.Prefix = prefix;

            return node;
        }

        public static string GetPrefix(string namespaceURI)
        {
            if (namespaceURI == "http://www.w3.org/2001/10/xml-exc-c14n#")
                return "ec";
            else if (namespaceURI == SignedXmlWithId.XmlDsigNamespaceUrl)
                return "ds";

            return string.Empty;
        }
    }
}
