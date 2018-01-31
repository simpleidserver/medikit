// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Medikit.Security.Cryptography;
using Medikit.Security.Cryptography.Asn1;

namespace Medikit.Security.Cryptography.Pkcs.Asn1
{
    [StructLayout(LayoutKind.Sequential)]
    public partial struct SignedDataAsn
    {
        public int Version;
        public AlgorithmIdentifierAsn[] DigestAlgorithms;
        public EncapsulatedContentInfoAsn EncapContentInfo;
        public CertificateChoiceAsn[] CertificateSet;
        public ReadOnlyMemory<byte>[] Crls;
        public SignerInfoAsn[] SignerInfos;

        internal void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushBerSequence();

            writer.WriteInteger(Version);

            writer.PushSetOf();
            for (int i = 0; i < DigestAlgorithms.Length; i++)
            {
                DigestAlgorithms[i].Encode(writer);
            }
            writer.PopSetOf();

            EncapContentInfo.Encode(writer);

            if (CertificateSet != null)
            {
                writer.PushTaggedObject(160);
                // writer.PushSetOf(new Asn1Tag(TagClass.ContextSpecific, 0));
                for (int i = 0; i < CertificateSet.Length; i++)
                {
                    CertificateSet[i].Encode(writer);
                }

                // writer.PopSetOf(new Asn1Tag(TagClass.ContextSpecific, 0));
                writer.PopTaggedObject();
            }


            if (Crls != null)
            {
                writer.PushSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
                for (int i = 0; i < Crls.Length; i++)
                {
                    writer.WriteEncodedValue(Crls[i].Span);
                }
                writer.PopSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
            }


            writer.PushSetOf();
            for (int i = 0; i < SignerInfos.Length; i++)
            {
                SignerInfos[i].Encode(writer);
            }

            writer.PopSetOf();
            writer.PopBerSequence();
        }

        internal static SignedDataAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static SignedDataAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            AsnValueReader reader = new AsnValueReader(encoded.Span, ruleSet);

            Decode(ref reader, expectedTag, encoded, out SignedDataAsn decoded);
            reader.ThrowIfNotEmpty();
            return decoded;
        }

        internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out SignedDataAsn decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out SignedDataAsn decoded)
        {
            decoded = default;
            AsnValueReader sequenceReader = reader.ReadSequence(expectedTag);
            AsnValueReader collectionReader;
            ReadOnlySpan<byte> rebindSpan = rebind.Span;
            int offset;
            ReadOnlySpan<byte> tmpSpan;


            if (!sequenceReader.TryReadInt32(out decoded.Version))
            {
                sequenceReader.ThrowIfNotEmpty();
            }


            // Decode SEQUENCE OF for DigestAlgorithms
            {
                collectionReader = sequenceReader.ReadSetOf();
                var tmpList = new List<AlgorithmIdentifierAsn>();
                AlgorithmIdentifierAsn tmpItem;

                while (collectionReader.HasData)
                {
                    AlgorithmIdentifierAsn.Decode(ref collectionReader, rebind, out tmpItem);
                    tmpList.Add(tmpItem);
                }

                decoded.DigestAlgorithms = tmpList.ToArray();
            }

            EncapsulatedContentInfoAsn.Decode(ref sequenceReader, rebind, out decoded.EncapContentInfo);
            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
            {

                // Decode SEQUENCE OF for CertificateSet
                {
                    collectionReader = sequenceReader.ReadSetOf(new Asn1Tag(TagClass.ContextSpecific, 0));
                    var tmpList = new List<CertificateChoiceAsn>();
                    CertificateChoiceAsn tmpItem;

                    while (collectionReader.HasData)
                    {
                        CertificateChoiceAsn.Decode(ref collectionReader, rebind, out tmpItem);
                        tmpList.Add(tmpItem);
                    }

                    decoded.CertificateSet = tmpList.ToArray();
                }

            }


            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
            {

                // Decode SEQUENCE OF for Crls
                {
                    collectionReader = sequenceReader.ReadSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
                    var tmpList = new List<ReadOnlyMemory<byte>>();
                    ReadOnlyMemory<byte> tmpItem;

                    while (collectionReader.HasData)
                    {
                        tmpSpan = collectionReader.ReadEncodedValue();
                        tmpItem = rebindSpan.Overlaps(tmpSpan, out offset) ? rebind.Slice(offset, tmpSpan.Length) : tmpSpan.ToArray();
                        tmpList.Add(tmpItem);
                    }

                    decoded.Crls = tmpList.ToArray();
                }

            }


            // Decode SEQUENCE OF for SignerInfos
            {
                collectionReader = sequenceReader.ReadSetOf();
                var tmpList = new List<SignerInfoAsn>();
                SignerInfoAsn tmpItem;

                while (collectionReader.HasData)
                {
                    SignerInfoAsn.Decode(ref collectionReader, rebind, out tmpItem);
                    tmpList.Add(tmpItem);
                }

                decoded.SignerInfos = tmpList.ToArray();
            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
