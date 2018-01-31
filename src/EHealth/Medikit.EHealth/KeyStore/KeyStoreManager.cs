// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.Extensions.Options;
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

        public X509Certificate2 GetIdAuthCertificate()
        {
            return GetCertificate(_options.IdentityCertificateStore, new Regex(AUTHENTICATION_CERT_NAME), _options.IdentityCertificateStorePassword);
        }

        public X509Certificate2 GetOrgAuthCertificate()
        {
            return GetCertificate(_options.OrgCertificateStore, new Regex(AUTHENTICATION_CERT_NAME), _options.OrgCertificateStorePassword);
        }

        public X509Certificate2 GetIdETKCertificate()
        {
            return GetCertificate(_options.IdentityCertificateStore, new Regex("[0-9]{13}"), _options.IdentityCertificateStorePassword);
        }
        
        public X509Certificate2 GetOrgETKCertificate()
        {
            return GetCertificate(_options.OrgCertificateStore, new Regex("[0-9]{13}"), _options.OrgCertificateStorePassword);
        }

        private static X509Certificate2 GetCertificate(string path, Regex regex, string password)
        {
            var col = new X509Certificate2Collection();
            col.Import(path, password, X509KeyStorageFlags.Exportable);
            foreach(var cert in col)
            {
                if(regex.IsMatch(cert.FriendlyName))
                {
                    return cert;
                }
            }

            return null;
        }
    }
}
