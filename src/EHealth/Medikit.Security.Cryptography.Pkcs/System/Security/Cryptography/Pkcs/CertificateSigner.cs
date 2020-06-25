// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Medikit.Security.Cryptography.Pkcs
{
    public class CertificateSigner : ISigner
    {
        private readonly MedikitCertificate _certificate;

        public CertificateSigner(MedikitCertificate certificate)
        {
            _certificate = certificate;
        }

        public bool Sign(ReadOnlySpan<byte> dataHash, HashAlgorithmName hashAlgorithmName, X509Certificate2 certificate, AsymmetricAlgorithm key, bool silent, out Oid oid, out ReadOnlyMemory<byte> signatureValue)
        {
            var privateKey = (RSA)_certificate.PrivateKey;
            oid = new Oid(Oids.RsaPkcs1Sha256);
            signatureValue = privateKey.SignHash(dataHash.ToArray(), hashAlgorithmName, RSASignaturePadding.Pss);
            return true;
        }
    }
}
