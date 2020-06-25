// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Medikit.Security.Cryptography.Pkcs
{
    public class MedikitCertificate
    {
        public MedikitCertificate(X509Certificate2 certificate, AsymmetricAlgorithm privateKey)
        {
            Certificate = certificate;
            PrivateKey = privateKey;
        }

        public X509Certificate2 Certificate { get; set; }
        public AsymmetricAlgorithm PrivateKey { get; set; }
    }
}
