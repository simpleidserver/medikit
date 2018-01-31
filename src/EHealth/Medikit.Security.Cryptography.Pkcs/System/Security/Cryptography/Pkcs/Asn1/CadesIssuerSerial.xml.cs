﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Medikit.Security.Cryptography;
using Medikit.Security.Cryptography.Asn1;

namespace Medikit.Security.Cryptography.Pkcs.Asn1
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct CadesIssuerSerial
    {
        internal GeneralNameAsn[] Issuer;
        internal ReadOnlyMemory<byte> SerialNumber;

        internal void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);


            writer.PushSequence();
            for (int i = 0; i < Issuer.Length; i++)
            {
                Issuer[i].Encode(writer);
            }
            writer.PopSequence();

            writer.WriteInteger(SerialNumber.Span);
            writer.PopSequence(tag);
        }

        internal static CadesIssuerSerial Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static CadesIssuerSerial Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            AsnValueReader reader = new AsnValueReader(encoded.Span, ruleSet);

            Decode(ref reader, expectedTag, encoded, out CadesIssuerSerial decoded);
            reader.ThrowIfNotEmpty();
            return decoded;
        }

        internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out CadesIssuerSerial decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out CadesIssuerSerial decoded)
        {
            decoded = default;
            AsnValueReader sequenceReader = reader.ReadSequence(expectedTag);
            AsnValueReader collectionReader;
            ReadOnlySpan<byte> rebindSpan = rebind.Span;
            int offset;
            ReadOnlySpan<byte> tmpSpan;


            // Decode SEQUENCE OF for Issuer
            {
                collectionReader = sequenceReader.ReadSequence();
                var tmpList = new List<GeneralNameAsn>();
                GeneralNameAsn tmpItem;

                while (collectionReader.HasData)
                {
                    GeneralNameAsn.Decode(ref collectionReader, rebind, out tmpItem);
                    tmpList.Add(tmpItem);
                }

                decoded.Issuer = tmpList.ToArray();
            }

            tmpSpan = sequenceReader.ReadIntegerBytes();
            decoded.SerialNumber = rebindSpan.Overlaps(tmpSpan, out offset) ? rebind.Slice(offset, tmpSpan.Length) : tmpSpan.ToArray();

            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
