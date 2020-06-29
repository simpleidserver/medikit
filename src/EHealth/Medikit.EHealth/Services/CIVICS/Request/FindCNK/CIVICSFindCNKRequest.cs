// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Xml.Linq;

namespace Medikit.EHealth.Services.CIVICS.Request
{
    public class CIVICSFindCNKRequest
    {
        public DateTime IssueInstant { get; set; }
        /// <summary>
        /// Partial or complete string referring to a medicinal product to be retrieved.
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// The version number.
        /// </summary>
        public long? Version { get; set; }
        /// <summary>
        /// The date starting from which information should be retrieved.
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// The name of the chapter involded in the reimbursement conditions of medicines example : 'IV'
        /// </summary>
        public string ChapterName { get; set; }
        /// <summary>
        /// The name of the paragraph or subparagraph which for the presumed chapter, is limited to an identification number. Examples : '30200'
        /// </summary>
        public string ParagraphName { get; set; }
        /// <summary>
        /// The language in which the redurect information is executed.
        /// </summary>
        public string Language { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.CIVICS2 + "FindCNKRequest",
                new XAttribute(XNamespace.Xmlns + "ns2", Constants.XMLNamespaces.CIVICS2),
                new XAttribute("IssueInstant", IssueInstant),
                new XAttribute(XNamespace.Xml + "lang", Language),
                new XElement("ProductName", ProductName));
            if (Version != null)
            {
                result.Add(new XElement("Version", Version));
            }

            if (StartDate != null)
            {
                result.Add(new XElement("StartDate", StartDate));
            }

            if (!string.IsNullOrWhiteSpace(ChapterName))
            {
                result.Add(new XElement("ChapterName", ChapterName));
            }

            if (!string.IsNullOrWhiteSpace(ParagraphName))
            {
                result.Add(new XElement("ParagraphName", ParagraphName));
            }

            return result;
        }
    }
}
