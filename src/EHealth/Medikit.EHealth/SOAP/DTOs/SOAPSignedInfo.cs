// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Xml;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    [XmlRoot(ElementName = "SignedInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public class SOAPSignedInfo
    {
        [XmlElement(ElementName = "CanonicalizationMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SOAPCanonicalizationMethod CanonicalizationMethod { get; set; }
        [XmlElement(ElementName = "SignatureMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SOAPSignatureMethod SignatureMethod { get; set; }
        [XmlElement(ElementName = "Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SOAPSignedInfoReference[] References { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.DS + "SignedInfo",
                CanonicalizationMethod.Serialize(),
                SignatureMethod.Serialize());
            foreach(var reference in References)
            {
                result.Add(reference.Serialize());
            }

            return result;
        }

        public byte[] ComputeSignature()
        {
            string xml = null;
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(SOAPSignedInfo));
                using (var writer = new FullElementXmlTextWriter(stream))
                {
                    serializer.Serialize(writer, this, namespaces);
                    xml = Encoding.UTF8.GetString(stream.ToArray());
                    xml = xml.Replace("﻿<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
                }
            }

            var payload = Encoding.UTF8.GetBytes(xml);
            return payload;
        }
    }
}
