// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.ETK
{
    public class ETKIdentifier
    {
        public static ETKIdentifier KGSS_ETK = new ETKIdentifier(Constants.EtkTypeToString[ETKTypes.CBE], "0809394427", "KGSS");
        public static ETKIdentifier RECIPE_ETK = new ETKIdentifier(Constants.EtkTypeToString[ETKTypes.CBE], "0823257311", "");

        public ETKIdentifier() { }

        public ETKIdentifier(string type, string value, string applicationId)
        {
            Type = type;
            Value = value;
            ApplicationId = applicationId;
        }

        [XmlElement("Type")]
        public string Type { get; set; }
        [XmlElement("Value")]
        public string Value { get; set; }
        [XmlElement("ApplicationID")]
        public string ApplicationId { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.ETK + "Identifier",
                new XElement(Constants.XMLNamespaces.ETK + "Type", Type),
                new XElement(Constants.XMLNamespaces.ETK + "Value", Value),
                new XElement(Constants.XMLNamespaces.ETK + "ApplicationID", ApplicationId));
            return result;
        }

        public override string ToString()
        {
            return $"{Type}-{Value}-{ApplicationId}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var target = obj as ETKIdentifier;
            if (target == null)
            {
                return false;
            }

            return target.GetHashCode() == this.GetHashCode();
        }
    }
}
