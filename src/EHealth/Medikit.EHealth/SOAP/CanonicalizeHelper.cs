// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SOAP.DTOs;
using Medikit.EHealth.Xml;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP
{
    public static class CanonicalizeHelper
    {
        public static SOAPSignedInfo Canonicalize(string xml, List<string> ids, List<Transform> transforms)
        {
            var doc = new XmlDsigDocument
            {
                PreserveWhitespace = false
            };
            doc.LoadXml(xml);
            var signedXml = new SignedXmlWithId(doc)
            {
                SigningKey = new RSACryptoServiceProvider()
            };
            signedXml.SignedInfo.CanonicalizationMethod = "http://www.w3.org/2001/10/xml-exc-c14n#";
            signedXml.SignedInfo.SignatureMethod = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
            foreach (var id in ids)
            {
                var reference = new Reference($"#{id}");
                foreach(var transform in transforms)
                {
                    reference.AddTransform(transform);
                }

                reference.DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha1";
                signedXml.AddReference(reference);
            }

            signedXml.ComputeSignature();
            var serializer = new XmlSerializer(typeof(SOAPSignedInfo));
            var outerXml = signedXml.GetXml().FirstChild.OuterXml;
            using (var reader = new StringReader(outerXml))
            {
                return (SOAPSignedInfo)serializer.Deserialize(reader);
            }
        }
    }
}
