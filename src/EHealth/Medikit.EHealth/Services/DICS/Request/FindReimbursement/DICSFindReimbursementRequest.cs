using System;
using System.Xml.Linq;

namespace Medikit.EHealth.Services.DICS.Request
{
    public class DICSFindReimbursementRequest
    {
        public DateTime IssueInstant { get; set; }
        /// <summary>
        /// Find the CNK codes corresponding to the matching packages, return the reimbursement contexts associated with these CNK codes.
        /// </summary>
        public DICSFindByPackage FindByPackage { get; set; }

        public XElement Serialize()
        {
            var result = new XElement(Constants.XMLNamespaces.DICSV5 + "FindReimbursementRequest",
                new XAttribute(XNamespace.Xmlns + "ns2", Constants.XMLNamespaces.DICSV5),
                new XAttribute(XNamespace.Xmlns + "ns3", Constants.XMLNamespaces.COMMONCORE),
                new XAttribute(XNamespace.Xmlns + "ns4", Constants.XMLNamespaces.COMMONPROTOCOL),
                new XAttribute("IssueInstant", IssueInstant));
            if (FindByPackage != null)
            {
                result.Add(FindByPackage.Serialize());
            }

            return result;
        }
    }
}
