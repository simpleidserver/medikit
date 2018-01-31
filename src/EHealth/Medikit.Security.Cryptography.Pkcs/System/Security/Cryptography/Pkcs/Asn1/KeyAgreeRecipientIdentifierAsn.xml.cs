﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable enable
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Medikit.Security.Cryptography;
using Medikit.Security.Cryptography.Asn1;

namespace Medikit.Security.Cryptography.Pkcs.Asn1
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct KeyAgreeRecipientIdentifierAsn
    {
        internal IssuerAndSerialNumberAsn? IssuerAndSerialNumber;
        internal RecipientKeyIdentifier? RKeyId;

#if DEBUG
        static KeyAgreeRecipientIdentifierAsn()
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

            ensureUniqueTag(Asn1Tag.Sequence, "IssuerAndSerialNumber");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 0), "RKeyId");
        }
#endif

        internal void Encode(AsnWriter writer)
        {
            bool wroteValue = false;

            if (IssuerAndSerialNumber.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                IssuerAndSerialNumber.Value.Encode(writer);
                wroteValue = true;
            }

            if (RKeyId.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                RKeyId.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 0));
                wroteValue = true;
            }

            if (!wroteValue)
            {
                throw new CryptographicException();
            }
        }

        internal static KeyAgreeRecipientIdentifierAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            AsnValueReader reader = new AsnValueReader(encoded.Span, ruleSet);

            Decode(ref reader, encoded, out KeyAgreeRecipientIdentifierAsn decoded);
            reader.ThrowIfNotEmpty();
            return decoded;
        }

        internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out KeyAgreeRecipientIdentifierAsn decoded)
        {
            decoded = default;
            Asn1Tag tag = reader.PeekTag();

            if (tag.HasSameClassAndValue(Asn1Tag.Sequence))
            {
                IssuerAndSerialNumberAsn tmpIssuerAndSerialNumber;
                IssuerAndSerialNumberAsn.Decode(ref reader, rebind, out tmpIssuerAndSerialNumber);
                decoded.IssuerAndSerialNumber = tmpIssuerAndSerialNumber;

            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
            {
                RecipientKeyIdentifier tmpRKeyId;
                RecipientKeyIdentifier.Decode(ref reader, new Asn1Tag(TagClass.ContextSpecific, 0), rebind, out tmpRKeyId);
                decoded.RKeyId = tmpRKeyId;

            }
            else
            {
                throw new CryptographicException();
            }
        }
    }
}
