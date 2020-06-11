// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.ETK;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Medikit.EHealth
{
    public class Constants
    {
        public static Dictionary<ETKTypes, string> EtkTypeToString = new Dictionary<ETKTypes, string>
        {
            { ETKTypes.CBE, "CBE" },
            { ETKTypes.NIHII, "NIHII" },
            { ETKTypes.NIHIIHOSPITAL, "NIHII-HOSPITAL" },
            { ETKTypes.NIHIIPHARMACY, "NIHII-PHARMACY" },
            { ETKTypes.SSIN, "SSIN" }
        };

        public static class AttributeStatementNames
        {
            public const string CertificateHolderPersonSSIN = "urn:be:fgov:ehealth:1.0:certificateholder:person:ssin";
            public const string PersonSSIN = "urn:be:fgov:person:ssin";
        }

        public static class AttributeStatementNamespaces
        {
            public const string Identification = "urn:be:fgov:identification-namespace";
            public const string Certified = "urn:be:fgov:certified-namespace:ehealth";
        }

        public static class Namespaces
        {
            public const string WSSE = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
            public const string WSU = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
            public const string SOAPENV = "http://schemas.xmlsoap.org/soap/envelope/";
            public const string DS = "http://www.w3.org/2000/09/xmldsig#";
            public const string SAML = "urn:oasis:names:tc:SAML:1.0:assertion";
            public const string SAMLP = "urn:oasis:names:tc:SAML:1.0:protocol";
            public const string ETK = "urn:be:fgov:ehealth:etkdepot:1_0:protocol";
            public const string CORE = "urn:be:fgov:ehealth:commons:1_0:core";
            public const string KGSS = "urn:be:fgov:ehealth:etee:kgss:1_0:protocol";
            public const string CONSULTRN = "urn:be:fgov:ehealth:consultRN:1_0:protocol";
            public const string DICSV5 = "urn:be:fgov:ehealth:dics:protocol:v5";
            public const string COMMONCORE = "urn:be:fgov:ehealth:commons:core:v2";
            public const string COMMONPROTOCOL = "urn:be:fgov:ehealth:commons:protocol:v2";
            public const string RECIPE = "urn:be:fgov:ehealth:recipe:protocol:v4";
            public const string WSSE11 = "http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd";
            public const string PRESCRIBER = "http:/services.recipe.be/prescriber";
            public const string PATIENT = "http:/services.recipe.be/patient";
            public const string EXECUTOR = "http:/services.recipe.be/executor";
        }

        public static class XMLNamespaces
        {
            public static XNamespace WSSE = Namespaces.WSSE;
            public static XNamespace WSU = Namespaces.WSU;
            public static XNamespace SOAPENV = Namespaces.SOAPENV;
            public static XNamespace DS = Namespaces.DS;
            public static XNamespace SAML = Namespaces.SAML;
            public static XNamespace SAMLP = Namespaces.SAMLP;
            public static XNamespace ETK = Namespaces.ETK;
            public static XNamespace KGSS = Namespaces.KGSS;
            public static XNamespace CONSULTRN = Namespaces.CONSULTRN;
            public static XNamespace DICSV5 = Namespaces.DICSV5;
            public static XNamespace COMMONCORE = Namespaces.COMMONCORE;
            public static XNamespace COMMONPROTOCOL = Namespaces.COMMONPROTOCOL;
            public static XNamespace RECIPE = Namespaces.RECIPE;
            public static XNamespace WSSE11 = Namespaces.WSSE11;
            public static XNamespace PRESCRIBER = Namespaces.PRESCRIBER;
            public static XNamespace PATIENT = Namespaces.PATIENT;
            public static XNamespace EXECUTOR = Namespaces.EXECUTOR;
        }
    }
}
