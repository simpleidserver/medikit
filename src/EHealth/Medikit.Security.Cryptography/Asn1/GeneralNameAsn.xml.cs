// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable enable
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Medikit.Security.Cryptography;
using Medikit.Security.Cryptography.Asn1;

namespace Medikit.Security.Cryptography.Asn1
{
    [StructLayout(LayoutKind.Sequential)]
    public partial struct GeneralNameAsn
    {
        public Medikit.Security.Cryptography.Asn1.OtherNameAsn? OtherName;
        public string Rfc822Name;
        public string DnsName;
        public ReadOnlyMemory<byte>? X400Address;
        public ReadOnlyMemory<byte>? DirectoryName;
        public Medikit.Security.Cryptography.Asn1.EdiPartyNameAsn? EdiPartyName;
        public string Uri;
        public ReadOnlyMemory<byte>? IPAddress;
        public string RegisteredId;

#if DEBUG
        static GeneralNameAsn()
        {
            var usedTags = new System.Collections.Generic.Dictionary<Asn1Tag, string>();
            Action<Asn1Tag, string> ensureUniqueTag = (tag, fieldName) =>
            {
                if (usedTags.TryGetValue(tag, out string? existing))
                {
                    throw new InvalidOperationException($"Tag '{tag}' is in use by both '{existing}' and '{fieldName}'");
                }

                usedTags.Add(tag, fieldName);
            };

            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 0), "OtherName");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 1), "Rfc822Name");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 2), "DnsName");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 3), "X400Address");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 4), "DirectoryName");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 5), "EdiPartyName");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 6), "Uri");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 7), "IPAddress");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 8), "RegisteredId");
        }
#endif

        public void Encode(AsnWriter writer)
        {
            bool wroteValue = false;

            if (OtherName.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                OtherName.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 0));
                wroteValue = true;
            }

            if (Rfc822Name != null)
            {
                if (wroteValue)
                    throw new CryptographicException();

                writer.WriteCharacterString(new Asn1Tag(TagClass.ContextSpecific, 1), UniversalTagNumber.IA5String, Rfc822Name);
                wroteValue = true;
            }

            if (DnsName != null)
            {
                if (wroteValue)
                    throw new CryptographicException();

                writer.WriteCharacterString(new Asn1Tag(TagClass.ContextSpecific, 2), UniversalTagNumber.IA5String, DnsName);
                wroteValue = true;
            }

            if (X400Address.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                // Validator for tag constraint for X400Address
                {
                    if (!Asn1Tag.TryDecode(X400Address.Value.Span, out Asn1Tag validateTag, out _) ||
                        !validateTag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 3)))
                    {
                        throw new CryptographicException();
                    }
                }

                writer.WriteEncodedValue(X400Address.Value.Span);
                wroteValue = true;
            }

            if (DirectoryName.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                writer.PushSequence(new Asn1Tag(TagClass.ContextSpecific, 4));
                writer.WriteEncodedValue(DirectoryName.Value.Span);
                writer.PopSequence(new Asn1Tag(TagClass.ContextSpecific, 4));
                wroteValue = true;
            }

            if (EdiPartyName.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                EdiPartyName.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 5));
                wroteValue = true;
            }

            if (Uri != null)
            {
                if (wroteValue)
                    throw new CryptographicException();

                writer.WriteCharacterString(new Asn1Tag(TagClass.ContextSpecific, 6), UniversalTagNumber.IA5String, Uri);
                wroteValue = true;
            }

            if (IPAddress.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                writer.WriteOctetString(new Asn1Tag(TagClass.ContextSpecific, 7), IPAddress.Value.Span);
                wroteValue = true;
            }

            if (RegisteredId != null)
            {
                if (wroteValue)
                    throw new CryptographicException();

                writer.WriteObjectIdentifier(new Asn1Tag(TagClass.ContextSpecific, 8), RegisteredId);
                wroteValue = true;
            }

            if (!wroteValue)
            {
                throw new CryptographicException();
            }
        }

        public static GeneralNameAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            AsnValueReader reader = new AsnValueReader(encoded.Span, ruleSet);

            Decode(ref reader, encoded, out GeneralNameAsn decoded);
            reader.ThrowIfNotEmpty();
            return decoded;
        }

        public static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out GeneralNameAsn decoded)
        {
            decoded = default;
            Asn1Tag tag = reader.PeekTag();
            AsnValueReader explicitReader;
            ReadOnlySpan<byte> rebindSpan = rebind.Span;
            int offset;
            ReadOnlySpan<byte> tmpSpan;

            if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
            {
                Medikit.Security.Cryptography.Asn1.OtherNameAsn tmpOtherName;
                Medikit.Security.Cryptography.Asn1.OtherNameAsn.Decode(ref reader, new Asn1Tag(TagClass.ContextSpecific, 0), rebind, out tmpOtherName);
                decoded.OtherName = tmpOtherName;

            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
            {
                decoded.Rfc822Name = reader.ReadCharacterString(new Asn1Tag(TagClass.ContextSpecific, 1), UniversalTagNumber.IA5String);
            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 2)))
            {
                decoded.DnsName = reader.ReadCharacterString(new Asn1Tag(TagClass.ContextSpecific, 2), UniversalTagNumber.IA5String);
            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 3)))
            {
                tmpSpan = reader.ReadEncodedValue();
                decoded.X400Address = rebindSpan.Overlaps(tmpSpan, out offset) ? rebind.Slice(offset, tmpSpan.Length) : tmpSpan.ToArray();
            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 4)))
            {
                explicitReader = reader.ReadSequence(new Asn1Tag(TagClass.ContextSpecific, 4));
                tmpSpan = explicitReader.ReadEncodedValue();
                decoded.DirectoryName = rebindSpan.Overlaps(tmpSpan, out offset) ? rebind.Slice(offset, tmpSpan.Length) : tmpSpan.ToArray();
                explicitReader.ThrowIfNotEmpty();
            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 5)))
            {
                Medikit.Security.Cryptography.Asn1.EdiPartyNameAsn tmpEdiPartyName;
                Medikit.Security.Cryptography.Asn1.EdiPartyNameAsn.Decode(ref reader, new Asn1Tag(TagClass.ContextSpecific, 5), rebind, out tmpEdiPartyName);
                decoded.EdiPartyName = tmpEdiPartyName;

            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 6)))
            {
                decoded.Uri = reader.ReadCharacterString(new Asn1Tag(TagClass.ContextSpecific, 6), UniversalTagNumber.IA5String);
            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 7)))
            {

                if (reader.TryReadPrimitiveOctetStringBytes(new Asn1Tag(TagClass.ContextSpecific, 7), out tmpSpan))
                {
                    decoded.IPAddress = rebindSpan.Overlaps(tmpSpan, out offset) ? rebind.Slice(offset, tmpSpan.Length) : tmpSpan.ToArray();
                }
                else
                {
                    decoded.IPAddress = reader.ReadOctetString(new Asn1Tag(TagClass.ContextSpecific, 7));
                }

            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 8)))
            {
                decoded.RegisteredId = reader.ReadObjectIdentifierAsString(new Asn1Tag(TagClass.ContextSpecific, 8));
            }
            else
            {
                throw new CryptographicException();
            }
        }
    }
}
