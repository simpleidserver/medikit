using Medikit.EHealth.KeyStore;
using Medikit.Security.Cryptography.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Medikit.Mobile.Services
{
    public class MobileKeyStoreManager : IKeyStoreManager
    {
        private const string AUTHENTICATION_CERT_NAME = "authentication";
        private readonly ICertificateStore _certificateStore;

        public MobileKeyStoreManager(ICertificateStore certificateStore)
        {
            _certificateStore = certificateStore;
        }

        public MedikitCertificate GetIdAuthCertificate()
        {
            var certs = _certificateStore.GetIdentityCertificates();
            var identityCertificate = _certificateStore.GetIdentityCertificates().Result.FirstOrDefault(_ => _.IsSelected);
            if (identityCertificate == null)
            {
                return null;
            }

            return GetCertificate(Convert.FromBase64String(identityCertificate.Payload), new Regex(AUTHENTICATION_CERT_NAME), identityCertificate.Password);
        }

        public MedikitCertificate GetIdETKCertificate()
        {
            var identityCertificate = _certificateStore.GetIdentityCertificates().Result.FirstOrDefault(_ => _.IsSelected);
            if (identityCertificate == null)
            {
                return null;
            }

            return GetCertificate(Convert.FromBase64String(identityCertificate.Payload), new Regex("[0-9]{13}"), identityCertificate.Password);
        }

        public MedikitCertificate GetOrgAuthCertificate()
        {
            var orgCertificate = _certificateStore.GetOrgCertificate().Result;
            if (orgCertificate == null)
            {
                return null;
            }

            return GetCertificate(Convert.FromBase64String(orgCertificate.Payload), new Regex(AUTHENTICATION_CERT_NAME), orgCertificate.Password);
        }

        public MedikitCertificate GetOrgETKCertificate()
        {
            var orgCertificate = _certificateStore.GetOrgCertificate().Result;
            if (orgCertificate == null)
            {
                return null;
            }

            return GetCertificate(Convert.FromBase64String(orgCertificate.Payload), new Regex("[0-9]{13}"), orgCertificate.Password);
        }

        private MedikitCertificate GetCertificate(byte[] payload, Regex regex, string password)
        {
            var store = new Pkcs12Store(new MemoryStream(payload), password.ToCharArray());
            string al = null;
            foreach (string alias in store.Aliases)
            {
                if (regex.IsMatch(alias))
                {
                    al = alias;
                    break;
                }
            }

            var cert = store.GetCertificate(al);
            var privateKey = Extract(store.GetKey(al).Key);
            var tmpCert = new X509Certificate(cert.Certificate.GetEncoded());
            var certificate = new X509Certificate2(tmpCert);
            return new MedikitCertificate(certificate, privateKey);
        }

        private static RSA Extract(AsymmetricKeyParameter parameter)
        {
            var result = RSA.Create();
            result.ImportParameters(ToRSAParameters((RsaPrivateCrtKeyParameters)parameter));
            return result;
        }

        private static RSAParameters ToRSAParameters(RsaPrivateCrtKeyParameters privKey)
        {
            RSAParameters rp = new RSAParameters();
            rp.Modulus = privKey.Modulus.ToByteArrayUnsigned();
            rp.Exponent = privKey.PublicExponent.ToByteArrayUnsigned();
            rp.P = privKey.P.ToByteArrayUnsigned();
            rp.Q = privKey.Q.ToByteArrayUnsigned();
            rp.D = ConvertRSAParametersField(privKey.Exponent, rp.Modulus.Length);
            rp.DP = ConvertRSAParametersField(privKey.DP, rp.P.Length);
            rp.DQ = ConvertRSAParametersField(privKey.DQ, rp.Q.Length);
            rp.InverseQ = ConvertRSAParametersField(privKey.QInv, rp.Q.Length);
            return rp;
        }

        private static byte[] ConvertRSAParametersField(Org.BouncyCastle.Math.BigInteger n, int size)
        {
            byte[] bs = n.ToByteArrayUnsigned();
            if (bs.Length == size)
                return bs;
            if (bs.Length > size)
                throw new ArgumentException("Specified size too small", "size");
            byte[] padded = new byte[size];
            Array.Copy(bs, 0, padded, size - bs.Length, bs.Length);
            return padded;
        }
    }
}
