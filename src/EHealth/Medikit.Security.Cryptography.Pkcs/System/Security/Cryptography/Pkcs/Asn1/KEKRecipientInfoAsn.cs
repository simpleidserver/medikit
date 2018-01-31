// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using Medikit.Security.Cryptography.Asn1;
using System;

namespace Medikit.Security.Cryptography.Pkcs.Asn1
{
    public partial struct KEKRecipientInfoAsn
    {
        public int Version { get; set; }
        public KEKIdentifierAsn KEKId { get; set; }
        public AlgorithmIdentifierAsn KeyEncryptionAlg { get; set; }
        public ReadOnlyMemory<byte> EncryptedKey { get; set; }

        public void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        public void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);
            writer.WriteInteger(Version);
            KEKId.Encode(writer);
            KeyEncryptionAlg.Encode(writer);
            writer.WriteOctetString(EncryptedKey.Span);
            writer.PopSequence(tag);
        }

        internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out KEKRecipientInfoAsn decoded)
        {
            decoded = default;
            KEKIdentifierAsn kekIdentifier;
            AlgorithmIdentifierAsn algIdentifier;
            AsnValueReader sequenceReader = reader.ReadSequence(expectedTag);
            int version;
            if (sequenceReader.TryReadInt32(out version))
            {
                decoded.Version = version;
            }


            KEKIdentifierAsn.Decode(ref sequenceReader, Asn1Tag.Sequence, rebind, out kekIdentifier);
            AlgorithmIdentifierAsn.Decode(ref sequenceReader, Asn1Tag.Sequence, rebind, out algIdentifier);
            var encrytpedKey = sequenceReader.ReadOctetString();

            decoded.KEKId = kekIdentifier;
            decoded.KeyEncryptionAlg = algIdentifier;
            decoded.EncryptedKey = encrytpedKey;
        }
    }
}
