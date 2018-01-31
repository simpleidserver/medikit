// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Extensions;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Medikit.EHealth.SOAP.DTOs
{
    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", ElementName = "Envelope")]
    public class SOAPEnvelope<T> where T : SOAPBody
    {
        public SOAPEnvelope()
        {

        }

        [XmlElement("Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public SOAPHeader Header { get; set; }
        [XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public T Body { get; set; }

        public virtual XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.SOAPENV + "Envelope",
                new XAttribute(XNamespace.Xmlns + "SOAP-ENV", Constants.XMLNamespaces.SOAPENV));
            if (Header != null)
            {
                result.Add(Header.Serialize());
            }

            result.Add(Body.Serialize());
            return result;
        }

        public string Serialize(bool omitXmlDeclaration)
        {
            var elt = Serialize();
            return elt.SerializeToString(omitXmlDeclaration);
        }
        
        public static SOAPEnvelope<T> Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(SOAPEnvelope<T>));
            SOAPEnvelope<T> samlEnv = null;
            using (var reader = new StringReader(xml))
            {
                samlEnv = (SOAPEnvelope<T>)serializer.Deserialize(reader);
            }

            return samlEnv;
        }
    }
}
