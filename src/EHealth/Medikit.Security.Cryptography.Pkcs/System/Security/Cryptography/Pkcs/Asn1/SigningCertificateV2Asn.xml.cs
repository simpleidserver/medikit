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
    internal partial struct SigningCertificateV2Asn
    {
        internal Asn1.EssCertIdV2[] Certs;
        internal Asn1.PolicyInformation[] Policies;

        internal void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);


            writer.PushSequence();
            for (int i = 0; i < Certs.Length; i++)
            {
                Certs[i].Encode(writer);
            }
            writer.PopSequence();


            if (Policies != null)
            {

                writer.PushSequence();
                for (int i = 0; i < Policies.Length; i++)
                {
                    Policies[i].Encode(writer);
                }
                writer.PopSequence();

            }

            writer.PopSequence(tag);
        }

        internal static SigningCertificateV2Asn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static SigningCertificateV2Asn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            AsnValueReader reader = new AsnValueReader(encoded.Span, ruleSet);

            Decode(ref reader, expectedTag, encoded, out SigningCertificateV2Asn decoded);
            reader.ThrowIfNotEmpty();
            return decoded;
        }

        internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out SigningCertificateV2Asn decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out SigningCertificateV2Asn decoded)
        {
            decoded = default;
            AsnValueReader sequenceReader = reader.ReadSequence(expectedTag);
            AsnValueReader collectionReader;


            // Decode SEQUENCE OF for Certs
            {
                collectionReader = sequenceReader.ReadSequence();
                var tmpList = new List<EssCertIdV2>();
                EssCertIdV2 tmpItem;

                while (collectionReader.HasData)
                {
                    EssCertIdV2.Decode(ref collectionReader, rebind, out tmpItem);
                    tmpList.Add(tmpItem);
                }

                decoded.Certs = tmpList.ToArray();
            }


            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(Asn1Tag.Sequence))
            {

                // Decode SEQUENCE OF for Policies
                {
                    collectionReader = sequenceReader.ReadSequence();
                    var tmpList = new List<PolicyInformation>();
                    PolicyInformation tmpItem;

                    while (collectionReader.HasData)
                    {
                        PolicyInformation.Decode(ref collectionReader, rebind, out tmpItem);
                        tmpList.Add(tmpItem);
                    }

                    decoded.Policies = tmpList.ToArray();
                }

            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
