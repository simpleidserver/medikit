// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;

namespace Medikit.EHealth.Services.Recipe.Request
{
    public class ListPrescriptionHistoryParameter
    {
        public string PatientId { get; set; }
        public string SymmKey { get; set; }
        public Page Page { get; set; }
        public bool ActiveResults { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.PRESCRIBER + "listRidsHistoryParam",
                new XAttribute(XNamespace.Xmlns + "ns2", Constants.Namespaces.PRESCRIBER),
                new XAttribute(XNamespace.Xmlns + "ns3", Constants.Namespaces.PATIENT),
                new XAttribute(XNamespace.Xmlns + "ns4", Constants.Namespaces.EXECUTOR));
            if (!string.IsNullOrWhiteSpace(PatientId))
            {
                result.Add(new XElement("patientId", PatientId));
            }

            if (!string.IsNullOrWhiteSpace(SymmKey))
            {
                result.Add(new XElement("symmKey", SymmKey));
            }

            result.Add(new XElement("activeResults", ActiveResults));

            if (Page != null)
            {
                result.Add(Page.Serialize());
            }

            return result;
        }
    }
}
