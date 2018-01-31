// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;

namespace Medikit.EHealth.Services.KGSS.Request
{
    public class KGSSGetKeyRequestContent
    {
        public string KeyIdentifier { get; set; }
        public string KeyEncryptionKey { get; set; }
        public string KeyEncryptionKeyIdentifier { get; set; }
        public string ETK { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.KGSS + "GetKeyRequestContent",
                new XAttribute("xmlns", Constants.Namespaces.KGSS));
            if (!string.IsNullOrWhiteSpace(KeyIdentifier))
            {
                result.Add(new XElement(Constants.XMLNamespaces.KGSS + "KeyIdentifier", KeyIdentifier));
            }

            if (!string.IsNullOrWhiteSpace(KeyEncryptionKey))
            {
                result.Add(new XElement(Constants.XMLNamespaces.KGSS + "KeyEncryptionKey", KeyIdentifier));
            }

            if (!string.IsNullOrWhiteSpace(KeyEncryptionKeyIdentifier))
            {
                result.Add(new XElement(Constants.XMLNamespaces.KGSS + "KeyEncryptionKeyIdentifier", KeyIdentifier));
            }

            if (!string.IsNullOrWhiteSpace(ETK))
            {
                result.Add(new XElement(Constants.XMLNamespaces.KGSS + "ETK", ETK));
            }

            return result;
        }
    }
}
