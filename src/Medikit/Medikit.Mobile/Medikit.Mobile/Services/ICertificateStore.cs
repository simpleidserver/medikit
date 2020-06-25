using Medikit.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medikit.Mobile.Services
{
    public interface ICertificateStore
    {
        Task<List<MedikitCertificate>> GetAll();
        Task<List<MedikitCertificate>> GetIdentityCertificates();
        Task<MedikitCertificate> GetOrgCertificate();
        Task<MedikitCertificate> Get(string name);
        Task<int> Add(MedikitCertificate certificate);
        Task<int> Update(MedikitCertificate certificate);
        Task<int> Remove(string name);
    }
}
