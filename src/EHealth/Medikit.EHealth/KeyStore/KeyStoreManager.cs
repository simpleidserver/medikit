// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Security.Cryptography.Pkcs;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Medikit.EHealth.KeyStore
{
    public class KeyStoreManager : IKeyStoreManager
    {
        private const string AUTHENTICATION_CERT_NAME = "authentication";
        private readonly EHealthOptions _options;

        public KeyStoreManager(IOptions<EHealthOptions> options)
        {
            _options = options.Value;
        }

        public MedikitCertificate GetIdAuthCertificate()
        {
            return GetCertificate(_options.IdentityCertificateStore, new Regex(AUTHENTICATION_CERT_NAME), _options.IdentityCertificateStorePassword);
        }

        public MedikitCertificate GetOrgAuthCertificate()
        {
            return GetCertificate(_options.OrgCertificateStore, new Regex(AUTHENTICATION_CERT_NAME), _options.OrgCertificateStorePassword);
        }

        public MedikitCertificate GetIdETKCertificate()
        {
            return GetCertificate(_options.IdentityCertificateStore, new Regex("[0-9]{13}"), _options.IdentityCertificateStorePassword);
        }

        public MedikitCertificate GetOrgETKCertificate()
        {
            return GetCertificate(_options.OrgCertificateStore, new Regex("[0-9]{13}"), _options.OrgCertificateStorePassword);
        }

        private static MedikitCertificate GetCertificate(string path, Regex regex, string password)
        {
            var store = new Pkcs12Store(new MemoryStream(File.ReadAllBytes(path)), password.ToCharArray());
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
            var key = (RsaPrivateCrtKeyParameters)store.GetKey(al).Key;
            var rsa = RSA.Create();
            rsa.ImportParameters(ToRSAParameters(key));
            var certificate = new X509Certificate2(cert.Certificate.GetEncoded(), password, X509KeyStorageFlags.PersistKeySet);
            certificate = certificate.CopyWithPrivateKey(rsa);
            return new MedikitCertificate(certificate, rsa);
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
