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
    internal partial struct Pbkdf2Params
    {
        private static ReadOnlySpan<byte> DefaultPrf => new byte[] { 0x30, 0x0C, 0x06, 0x08, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x02, 0x07, 0x05, 0x00 };

        internal Medikit.Security.Cryptography.Asn1.Pbkdf2SaltChoice Salt;
        internal int IterationCount;
        internal byte? KeyLength;
        internal Medikit.Security.Cryptography.Asn1.AlgorithmIdentifierAsn Prf;

#if DEBUG
        static Pbkdf2Params()
        {
            Pbkdf2Params decoded = default;
            ReadOnlyMemory<byte> rebind = default;
            AsnValueReader reader;

            reader = new AsnValueReader(DefaultPrf, AsnEncodingRules.DER);
            Medikit.Security.Cryptography.Asn1.AlgorithmIdentifierAsn.Decode(ref reader, rebind, out decoded.Prf);
            reader.ThrowIfNotEmpty();
        }
#endif

        internal void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);

            Salt.Encode(writer);
            writer.WriteInteger(IterationCount);

            if (KeyLength.HasValue)
            {
                writer.WriteInteger(KeyLength.Value);
            }


            // DEFAULT value handler for Prf.
            {
                using (AsnWriter tmp = new AsnWriter(AsnEncodingRules.DER))
                {
                    Prf.Encode(tmp);
                    ReadOnlySpan<byte> encoded = tmp.EncodeAsSpan();

                    if (!encoded.SequenceEqual(DefaultPrf))
                    {
                        writer.WriteEncodedValue(encoded);
                    }
                }
            }

            writer.PopSequence(tag);
        }

        internal static Pbkdf2Params Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static Pbkdf2Params Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            AsnValueReader reader = new AsnValueReader(encoded.Span, ruleSet);

            Decode(ref reader, expectedTag, encoded, out Pbkdf2Params decoded);
            reader.ThrowIfNotEmpty();
            return decoded;
        }

        internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out Pbkdf2Params decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out Pbkdf2Params decoded)
        {
            decoded = default;
            AsnValueReader sequenceReader = reader.ReadSequence(expectedTag);
            AsnValueReader defaultReader;

            Medikit.Security.Cryptography.Asn1.Pbkdf2SaltChoice.Decode(ref sequenceReader, rebind, out decoded.Salt);

            if (!sequenceReader.TryReadInt32(out decoded.IterationCount))
            {
                sequenceReader.ThrowIfNotEmpty();
            }


            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(Asn1Tag.Integer))
            {

                if (sequenceReader.TryReadUInt8(out byte tmpKeyLength))
                {
                    decoded.KeyLength = tmpKeyLength;
                }
                else
                {
                    sequenceReader.ThrowIfNotEmpty();
                }

            }


            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(Asn1Tag.Sequence))
            {
                Medikit.Security.Cryptography.Asn1.AlgorithmIdentifierAsn.Decode(ref sequenceReader, rebind, out decoded.Prf);
            }
            else
            {
                defaultReader = new AsnValueReader(DefaultPrf, AsnEncodingRules.DER);
                Medikit.Security.Cryptography.Asn1.AlgorithmIdentifierAsn.Decode(ref defaultReader, rebind, out decoded.Prf);
            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
