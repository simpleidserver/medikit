// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable enable
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Medikit.Security.Cryptography.Asn1;

namespace Medikit.Security.Cryptography.Pkcs.Asn1
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct RecipientInfoAsn
    {
        internal KeyTransRecipientInfoAsn? Ktri;
        internal KeyAgreeRecipientInfoAsn? Kari;
        internal KEKRecipientInfoAsn? KrecipientInfo;

#if DEBUG
        static RecipientInfoAsn()
        {
            var usedTags = new Dictionary<Asn1Tag, string>();
            Action<Asn1Tag, string> ensureUniqueTag = (tag, fieldName) =>
            {
                if (usedTags.TryGetValue(tag, out string? existing))
                {
                    throw new InvalidOperationException($"Tag '{tag}' is in use by both '{existing}' and '{fieldName}'");
                }

                usedTags.Add(tag, fieldName);
            };

            ensureUniqueTag(Asn1Tag.Sequence, "Ktri");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 1), "Kari");
        }
#endif

        internal void Encode(AsnWriter writer)
        {
            bool wroteValue = false;

            if (Ktri.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                Ktri.Value.Encode(writer);
                wroteValue = true;
            }

            if (Kari.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                Kari.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 1));
                wroteValue = true;
            }

            if (KrecipientInfo.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                KrecipientInfo.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 2));
                wroteValue = true;
            }

            if (!wroteValue)
            {
                throw new CryptographicException();
            }
        }

        internal static RecipientInfoAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            AsnValueReader reader = new AsnValueReader(encoded.Span, ruleSet);

            Decode(ref reader, encoded, out RecipientInfoAsn decoded);
            reader.ThrowIfNotEmpty();
            return decoded;
        }

        internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out RecipientInfoAsn decoded)
        {
            decoded = default;
            Asn1Tag tag = reader.PeekTag();
            if (tag.HasSameClassAndValue(Asn1Tag.Sequence))
            {
                KeyTransRecipientInfoAsn tmpKtri;
                KeyTransRecipientInfoAsn.Decode(ref reader, rebind, out tmpKtri);
                decoded.Ktri = tmpKtri;
            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
            {
                KeyAgreeRecipientInfoAsn tmpKari;
                KeyAgreeRecipientInfoAsn.Decode(ref reader, new Asn1Tag(TagClass.ContextSpecific, 1), rebind, out tmpKari);
                decoded.Kari = tmpKari;
            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 2)))
            {
                KEKRecipientInfoAsn kekRecipientInfo;
                KEKRecipientInfoAsn.Decode(ref reader, new Asn1Tag(TagClass.ContextSpecific, 2), rebind, out kekRecipientInfo);
                decoded.KrecipientInfo = kekRecipientInfo;
            }
            else
            {
                throw new CryptographicException();
            }
        }
    }
}
