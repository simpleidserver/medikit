// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Security.Cryptography;
using Medikit.Security.Cryptography.Asn1;
using Medikit.Security.Cryptography.Pkcs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Medikit.EHealth.Pkcs
{
    public class TripleWrapper
    {
        public static byte[] Seal(byte[] payload, X509Certificate2 senderCertificate, X509Certificate2 recipientCertificate)
        {
            var lst = new List<X509Certificate2>
            {
                senderCertificate
            };
            var signer = new CertificateSigner(senderCertificate);
            var inner = Sign(payload, senderCertificate, null, SigningPolicy.EHEALTH_CERT, signer);
            var envelopedMessage = Encrypt(inner, recipientCertificate);
            var outer = Sign(envelopedMessage, senderCertificate, lst, SigningPolicy.EHEALTH_CERT, signer);
            return outer;
        }

        public static byte[] Seal(byte[] payload, X509Certificate2 senderCertificate, string keyId, SymmetricAlgorithm symmetricAlg)
        {
            var lst = new List<X509Certificate2>
            {
                senderCertificate
            };
            var signer = new CertificateSigner(senderCertificate);
            var inner = Sign(payload, senderCertificate, null, SigningPolicy.EHEALTH_CERT, signer);
            var envelopedMessage = Encrypt(inner, symmetricAlg, keyId);
            var outer = Sign(envelopedMessage, senderCertificate, lst, SigningPolicy.EHEALTH_CERT, signer);
            return outer;
        }

        public static byte[] Unseal(string sealedContent, X509Certificate2Collection col)
        {
            return Unseal(Convert.FromBase64String(sealedContent), col);
        }

        public static byte[] Unseal(byte[] payload, X509Certificate2Collection col)
        {
            var unsigned = Unsign(payload);
            var uncrypted = Decrypt(unsigned, col);
            return Unsign(uncrypted);
        }

        public static byte[] Unseal(byte[] payload, byte[] key)
        {
            var unsigned = Unsign(payload);
            var uncrypted = Decrypt(unsigned, key);
            return Unsign(uncrypted);
        }

        private static byte[] Sign(byte[] contentInfoPayload, X509Certificate2 certificate, List<X509Certificate2> chain, SigningPolicy signingPolicy, ISigner signer)
        {
            var signedCms = new SignedCms(new ContentInfo(contentInfoPayload));
            var cmsSigner = new CmsSigner(signer, signingPolicy, certificate)
            {
                IncludeOption = X509IncludeOption.None
            };

            if (chain != null)
            {
                foreach(var cert in chain)
                {
                    cmsSigner.Certificates.Add(cert);
                }
            }

            cmsSigner.AddAuthenticationTime(DateTime.UtcNow);
            cmsSigner.AddAlgorithmProtectAttribute();
            signedCms.ComputeSignature(cmsSigner);
            return signedCms.Encode();
        }

        private static byte[] Encrypt(byte[] contentInfoPayload, X509Certificate2 certificate)
        {
            var envelopedCms = new EnvelopedCms(new ContentInfo(contentInfoPayload), new AlgorithmIdentifier(Oid.FromOidValue(Oids.Aes128Cbc, OidGroup.EncryptionAlgorithm)));
            envelopedCms.Encrypt(new CmsRecipient(certificate));
            var payload = envelopedCms.Encode();
            return payload;
        }

        private static byte[] Encrypt(byte[] contentInfoPayload, SymmetricAlgorithm symmetricAlg, string keyId)
        {
            var envelopedCms = new EnvelopedCms(new ContentInfo(contentInfoPayload), new AlgorithmIdentifier(Oid.FromOidValue(Oids.Aes128Cbc, OidGroup.EncryptionAlgorithm)));
            envelopedCms.Encrypt(new CmsRecipient(keyId, symmetricAlg));
            var payload = envelopedCms.Encode();
            return payload;
        }

        private static byte[] Unsign(byte[] payload)
        {
            var signedCms = new SignedCms();
            signedCms.Decode(payload);
            return signedCms.ContentInfo.Content;
        }

        private static byte[] Decrypt(byte[] payload, X509Certificate2Collection col)
        {
            var enveloped = new EnvelopedCms();
            enveloped.Decode(payload);
            enveloped.Decrypt(col);
            return enveloped.ContentInfo.Content;
        }

        private static byte[] Decrypt(byte[] payload, byte[] key)
        {
            var enveloped = new EnvelopedCms();
            enveloped.Decode(payload);
            var recipientInfo = enveloped.RecipientInfos[0];
            var unwrappedKey = KeyWrapAlgorithm.UnwrapKey(key, recipientInfo.EncryptedKey);
            using (var aes = Aes.Create())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                aes.KeySize = 128;
                aes.Key = unwrappedKey;
                AsnReader reader = new AsnReader(enveloped.ContentEncryptionAlgorithm.Parameters, AsnEncodingRules.BER);
                if (reader.TryReadPrimitiveOctetStringBytes(out ReadOnlyMemory<byte> primitiveBytes))
                {
                    aes.IV = primitiveBytes.ToArray();
                }

                using (var decryptor = aes.CreateDecryptor(unwrappedKey, aes.IV))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(enveloped.ContentInfo.Content, 0, enveloped.ContentInfo.Content.Length);
                        }

                        var result = memoryStream.ToArray();
                        return result;
                    }
                }
            }
        }
    }
}