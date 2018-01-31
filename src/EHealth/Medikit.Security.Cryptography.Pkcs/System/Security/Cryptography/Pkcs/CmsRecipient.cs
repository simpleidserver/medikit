// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Medikit.Security.Cryptography.Pkcs.Resources;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Medikit.Security.Cryptography.Pkcs
{
    public enum CmsRecipientTypes
    {
        KeyTransRecipientInfoAsn = 0,
        KEKRecipientInfo = 1
    }

    public sealed class CmsRecipient
    {
        public CmsRecipient(X509Certificate2 certificate)
            : this(SubjectIdentifierType.IssuerAndSerialNumber, certificate)
        {
        }

        public CmsRecipient(string keyId, SymmetricAlgorithm symmetricAlogrithm)
        {
            SymmetricKeyId = keyId;
            SymmetricAlgorithm = symmetricAlogrithm;
            Type = CmsRecipientTypes.KEKRecipientInfo;
        }

#if NETSTANDARD2_0
        internal
#else
        public
#endif
        CmsRecipient(X509Certificate2 certificate, RSAEncryptionPadding rsaEncryptionPadding)
            : this(certificate)
        {
            ValidateRSACertificate(certificate);
            RSAEncryptionPadding = rsaEncryptionPadding ?? throw new ArgumentNullException(nameof(rsaEncryptionPadding));
        }

#if NETSTANDARD2_0
        internal
#else
        public
#endif
        CmsRecipient(SubjectIdentifierType recipientIdentifierType, X509Certificate2 certificate, RSAEncryptionPadding rsaEncryptionPadding)
            : this(recipientIdentifierType, certificate)
        {
            ValidateRSACertificate(certificate);
            RSAEncryptionPadding = rsaEncryptionPadding ?? throw new ArgumentNullException(nameof(rsaEncryptionPadding));
            Type = CmsRecipientTypes.KeyTransRecipientInfoAsn;
        }

        public CmsRecipient(SubjectIdentifierType recipientIdentifierType, X509Certificate2 certificate)
        {
            if (certificate == null)
                throw new ArgumentNullException(nameof(certificate));

            switch (recipientIdentifierType)
            {
                case SubjectIdentifierType.Unknown:
                    recipientIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
                    break;
                case SubjectIdentifierType.IssuerAndSerialNumber:
                    break;
                case SubjectIdentifierType.SubjectKeyIdentifier:
                    break;
                default:
                    throw new CryptographicException(string.Format(Strings.Cryptography_Cms_Invalid_Subject_Identifier_Type, recipientIdentifierType));
            }

            RecipientIdentifierType = recipientIdentifierType;
            Certificate = certificate;
        }

#if NETSTANDARD2_0
        internal
#else
        public
#endif
        RSAEncryptionPadding? RSAEncryptionPadding { get; }
        public SubjectIdentifierType RecipientIdentifierType { get; }
        public X509Certificate2 Certificate { get; }
        public CmsRecipientTypes Type { get; }
        public SymmetricAlgorithm SymmetricAlgorithm { get; }
        public string SymmetricKeyId { get; }

        private static void ValidateRSACertificate(X509Certificate2 certificate)
        {
            switch (certificate.GetKeyAlgorithm())
            {
                case Oids.Rsa:
                case Oids.RsaOaep:
                    break;
                default:
                    throw new CryptographicException(Strings.Cryptography_Cms_Recipient_RSARequired_RSAPaddingModeSupplied);
            }
        }
    }
}
