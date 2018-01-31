// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using Medikit.Security.Cryptography.Asn1;
using System;
using System.Runtime.InteropServices;

namespace Medikit.Security.Cryptography.Pkcs.Asn1
{
    [StructLayout(LayoutKind.Sequential)]
    public partial struct KEKIdentifierAsn
    {
        public ReadOnlyMemory<byte> KeyIdentifier { get; set; }
        public DateTimeOffset? Date { get; set; }

        public void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        public void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);
            writer.WriteOctetString(KeyIdentifier.Span);
            if (Date != null)
            {
                writer.WriteGeneralizedTime(Date.Value);
            }

            writer.PopSequence(tag);
        }

        internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out KEKIdentifierAsn decoded)
        {
            decoded = default;
            AsnValueReader sequenceReader = reader.ReadSequence(expectedTag);
            decoded.KeyIdentifier = sequenceReader.ReadOctetString();
        }
    }
}
