﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Runtime.InteropServices;
using Medikit.Security.Cryptography;
using Medikit.Security.Cryptography.Asn1;

namespace Medikit.Security.Cryptography.Asn1
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct ECPrivateKey
    {
        internal byte Version;
        internal ReadOnlyMemory<byte> PrivateKey;
        internal Medikit.Security.Cryptography.Asn1.ECDomainParameters? Parameters;
        internal ReadOnlyMemory<byte>? PublicKey;

        internal void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);

            writer.WriteInteger(Version);
            writer.WriteOctetString(PrivateKey.Span);

            if (Parameters.HasValue)
            {
                writer.PushSequence(new Asn1Tag(TagClass.ContextSpecific, 0));
                Parameters.Value.Encode(writer);
                writer.PopSequence(new Asn1Tag(TagClass.ContextSpecific, 0));
            }


            if (PublicKey.HasValue)
            {
                writer.PushSequence(new Asn1Tag(TagClass.ContextSpecific, 1));
                writer.WriteBitString(PublicKey.Value.Span);
                writer.PopSequence(new Asn1Tag(TagClass.ContextSpecific, 1));
            }

            writer.PopSequence(tag);
        }

        internal static ECPrivateKey Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static ECPrivateKey Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            AsnValueReader reader = new AsnValueReader(encoded.Span, ruleSet);

            Decode(ref reader, expectedTag, encoded, out ECPrivateKey decoded);
            reader.ThrowIfNotEmpty();
            return decoded;
        }

        internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out ECPrivateKey decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out ECPrivateKey decoded)
        {
            decoded = default;
            AsnValueReader sequenceReader = reader.ReadSequence(expectedTag);
            AsnValueReader explicitReader;
            ReadOnlySpan<byte> rebindSpan = rebind.Span;
            int offset;
            ReadOnlySpan<byte> tmpSpan;


            if (!sequenceReader.TryReadUInt8(out decoded.Version))
            {
                sequenceReader.ThrowIfNotEmpty();
            }


            if (sequenceReader.TryReadPrimitiveOctetStringBytes(out tmpSpan))
            {
                decoded.PrivateKey = rebindSpan.Overlaps(tmpSpan, out offset) ? rebind.Slice(offset, tmpSpan.Length) : tmpSpan.ToArray();
            }
            else
            {
                decoded.PrivateKey = sequenceReader.ReadOctetString();
            }


            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
            {
                explicitReader = sequenceReader.ReadSequence(new Asn1Tag(TagClass.ContextSpecific, 0));
                Medikit.Security.Cryptography.Asn1.ECDomainParameters tmpParameters;
                Medikit.Security.Cryptography.Asn1.ECDomainParameters.Decode(ref explicitReader, rebind, out tmpParameters);
                decoded.Parameters = tmpParameters;

                explicitReader.ThrowIfNotEmpty();
            }


            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
            {
                explicitReader = sequenceReader.ReadSequence(new Asn1Tag(TagClass.ContextSpecific, 1));

                if (explicitReader.TryReadPrimitiveBitStringValue(out _, out tmpSpan))
                {
                    decoded.PublicKey = rebindSpan.Overlaps(tmpSpan, out offset) ? rebind.Slice(offset, tmpSpan.Length) : tmpSpan.ToArray();
                }
                else
                {
                    decoded.PublicKey = explicitReader.ReadBitString(out _);
                }

                explicitReader.ThrowIfNotEmpty();
            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
