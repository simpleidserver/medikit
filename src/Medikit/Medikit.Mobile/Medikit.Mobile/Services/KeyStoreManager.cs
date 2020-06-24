using System.Security.Cryptography.X509Certificates;
using Medikit.EHealth.KeyStore;

namespace Medikit.Mobile.Services
{
    public class KeyStoreManager : IKeyStoreManager
    {
        public X509Certificate2 GetIdAuthCertificate()
        {
            throw new System.NotImplementedException();
        }

        public X509Certificate2 GetIdETKCertificate()
        {
            throw new System.NotImplementedException();
        }

        public X509Certificate2 GetOrgAuthCertificate()
        {
            throw new System.NotImplementedException();
        }

        public X509Certificate2 GetOrgETKCertificate()
        {
            throw new System.NotImplementedException();
        }
    }
}
