// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Medikit.Security.Cryptography;
using Medikit.Security.Cryptography.Asn1;
using Medikit.Security.Cryptography.Asn1.Pkcs7;

namespace Medikit.Security.Cryptography.Pkcs.Asn1
{
    [StructLayout(LayoutKind.Sequential)]
    public partial struct EnvelopedDataAsn
    {
        internal int Version;
        internal OriginatorInfoAsn? OriginatorInfo;
        internal RecipientInfoAsn[] RecipientInfos;
        internal EncryptedContentInfoAsn EncryptedContentInfo;
        internal AttributeAsn[] UnprotectedAttributes;

        public void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        public void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushBerSequence();
            writer.WriteInteger(Version);
            if (OriginatorInfo.HasValue)
            {
                OriginatorInfo.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 0));
            }

            writer.PushSetOf();
            for (int i = 0; i < RecipientInfos.Length; i++)
            {
                RecipientInfos[i].Encode(writer);
            }

            writer.PopSetOf();
            EncryptedContentInfo.Encode(writer);
            if (UnprotectedAttributes != null)
            {

                writer.PushSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
                for (int i = 0; i < UnprotectedAttributes.Length; i++)
                {
                    UnprotectedAttributes[i].Encode(writer);
                }
                writer.PopSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
            }

            writer.PopBerSequence();
        }

        public static EnvelopedDataAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        public static EnvelopedDataAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            AsnValueReader reader = new AsnValueReader(encoded.Span, ruleSet);

            Decode(ref reader, expectedTag, encoded, out EnvelopedDataAsn decoded);
            reader.ThrowIfNotEmpty();
            return decoded;
        }

        public static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out EnvelopedDataAsn decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        public static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EnvelopedDataAsn decoded)
        {
            decoded = default;
            AsnValueReader sequenceReader = reader.ReadSequence(expectedTag);
            AsnValueReader collectionReader;


            if (!sequenceReader.TryReadInt32(out decoded.Version))
            {
                sequenceReader.ThrowIfNotEmpty();
            }


            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
            {
                OriginatorInfoAsn tmpOriginatorInfo;
                OriginatorInfoAsn.Decode(ref sequenceReader, new Asn1Tag(TagClass.ContextSpecific, 0), rebind, out tmpOriginatorInfo);
                decoded.OriginatorInfo = tmpOriginatorInfo;

            }


            // Decode SEQUENCE OF for RecipientInfos
            {
                collectionReader = sequenceReader.ReadSetOf();
                var tmpList = new List<RecipientInfoAsn>();
               RecipientInfoAsn tmpItem;

                while (collectionReader.HasData)
                {
                    RecipientInfoAsn.Decode(ref collectionReader, rebind, out tmpItem);
                    tmpList.Add(tmpItem);
                }

                decoded.RecipientInfos = tmpList.ToArray();
            }

            EncryptedContentInfoAsn.Decode(ref sequenceReader, rebind, out decoded.EncryptedContentInfo);

            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
            {

                // Decode SEQUENCE OF for UnprotectedAttributes
                {
                    collectionReader = sequenceReader.ReadSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
                    var tmpList = new List<AttributeAsn>();
                   AttributeAsn tmpItem;

                    while (collectionReader.HasData)
                    {
                        AttributeAsn.Decode(ref collectionReader, rebind, out tmpItem);
                        tmpList.Add(tmpItem);
                    }

                    decoded.UnprotectedAttributes = tmpList.ToArray();
                }

            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
