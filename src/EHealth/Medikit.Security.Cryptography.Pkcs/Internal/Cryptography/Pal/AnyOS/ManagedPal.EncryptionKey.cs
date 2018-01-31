// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Medikit.Security.Cryptography.Pkcs;
using Medikit.Security.Cryptography.Pkcs.Asn1;

namespace Internal.Cryptography.Pal.AnyOS
{
    internal sealed partial class ManagedPkcsPal : PkcsPal
    {
        internal sealed class ManagedEncryptionKeyPal : KeyTransRecipientInfoPal
        {
            private readonly KEKRecipientInfoAsn _asn;

            internal ManagedEncryptionKeyPal(KEKRecipientInfoAsn asn)
            {
                _asn = asn;
            }

            public override byte[] EncryptedKey => _asn.EncryptedKey.ToArray();

            public override AlgorithmIdentifier KeyEncryptionAlgorithm => _asn.KeyEncryptionAlg.ToPresentationObject();

            public override SubjectIdentifier RecipientIdentifier => new SubjectIdentifier(SubjectIdentifierType.SubjectKeyIdentifier, _asn.KEKId.KeyIdentifier);

            public override int Version => _asn.Version;
        }
    }
}
