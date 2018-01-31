// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Medikit.EHealth.Services.KGSS.Request
{
    public class KGSSGetNewKeyRequestContent
    {
        public KGSSGetNewKeyRequestContent()
        {
            AllowedReaders = new List<CredentialType>();
            ExcludedReaders = new List<CredentialType>();
        }

        /// <summary>
        /// A list of AllowedReaders. This list contains the readers that are allowed to obtain the newly created key.
        /// The allowed readers must be identified with the eHealth CredentialType.
        /// Ligne : 16
        /// </summary>
        public List<CredentialType> AllowedReaders { get; set; }
        /// <summary>
        /// A list of ExcludedReaders. This list containsthe readers that are explicitly excluded from getting the newly created key.
        /// The excluded readers must be identified with the eHealth CredentialType.
        /// </summary>
        public List<CredentialType> ExcludedReaders { get; set; }
        /// <summary>
        /// The ETK provided by the requestor that will be used by the KGSS to encrypt the response.
        /// This is usually the ETK of the message sender.
        /// </summary>
        public string ETK { get; set; }
        /// <summary>
        /// Reserved for later use to define in which circumstances a key can be delete.
        /// </summary>
        public string DeletionStrategy { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.KGSS + "GetNewKeyRequestContent",
                new XAttribute("xmlns", Constants.Namespaces.KGSS));
            foreach(var allowedReader in AllowedReaders)
            {
                result.Add(allowedReader.Serialize("AllowedReader"));
            }

            foreach(var excludedReader in ExcludedReaders)
            {
                result.Add(excludedReader.Serialize("ExcludedReader"));
            }

            result.Add(new XElement(Constants.XMLNamespaces.KGSS + "ETK", ETK));
            if (!string.IsNullOrWhiteSpace(DeletionStrategy))
            {
                result.Add(new XElement("DeletionStrategy", DeletionStrategy));
            }

            return result;
        }
    }
}
