using SQLite;
using System;

namespace Medikit.Mobile.Models
{
    public class MedikitCertificate
    {
        [PrimaryKey]
        public string Name { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Password { get; set; }
        public string Payload { get; set; }
        public bool IsSelected { get; set; }
        public MedikitCertificateTypes Type { get; set; }
    }
}
