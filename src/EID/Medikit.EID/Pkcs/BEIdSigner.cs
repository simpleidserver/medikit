// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Security.Cryptography.Pkcs;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Medikit.EID.Pkcs
{
    public class BEIdSigner : ISigner
    {
        private static Dictionary<HashAlgorithmName, BeIDDigest> MAPPING = new Dictionary<HashAlgorithmName, BeIDDigest>
        {
            { HashAlgorithmName.SHA256, BeIDDigest.Sha256 },
            { HashAlgorithmName.SHA384, BeIDDigest.Sha384 },
            { HashAlgorithmName.SHA512, BeIDDigest.Sha512 },
            { HashAlgorithmName.SHA1, BeIDDigest.Sha1 }
        };
        private readonly BeIDCardConnector _beIDCardConnector;
        private readonly string _pin;

        public BEIdSigner(BeIDCardConnector beIDCardConnector, string pin) 
        {
            _beIDCardConnector = beIDCardConnector;
            _pin = pin;
        }

        public bool Sign(ReadOnlySpan<byte> dataHash, HashAlgorithmName hashAlgorithmName, X509Certificate2 certificate, AsymmetricAlgorithm key, bool silent, out Oid oid, out ReadOnlyMemory<byte> signatureValue)
        {
            var result = _beIDCardConnector.SignWithAuthenticationCertificate(dataHash.ToArray(), GetDigest(hashAlgorithmName), _pin);
            oid = new Oid(Medikit.Security.Cryptography.Oids.RsaPkcs1Sha256);
            signatureValue = result;
            return true;
        }

        private static BeIDDigest GetDigest(HashAlgorithmName algName)
        {
            return MAPPING[algName];
        }
    }
}
