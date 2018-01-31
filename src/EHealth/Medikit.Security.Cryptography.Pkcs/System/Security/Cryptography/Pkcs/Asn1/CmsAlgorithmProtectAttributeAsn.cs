using System;
using System.Runtime.InteropServices;
using Medikit.Security.Cryptography.Asn1;

namespace Medikit.Security.Cryptography.Pkcs.Asn1
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CmsAlgorithmProtectAttributeAsn
    {
        public AlgorithmIdentifierAsn DigestAlgorithm;
        public AlgorithmIdentifierAsn? SignatureAlgorithm;
        public AlgorithmIdentifierAsn? MacAlgorithm;

        public void Encode(AsnWriter writer)
        {
            writer.PushSequence();
            DigestAlgorithm.Encode(writer);
            if(SignatureAlgorithm != null)
            {
                SignatureAlgorithm.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 1));
            }
            else
            {
                MacAlgorithm.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 2));
            }

            writer.PopSequence();
        }

        public static CmsAlgorithmProtectAttributeAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            AsnValueReader reader = new AsnValueReader(encoded.Span, ruleSet);
            Decode(ref reader, encoded, out CmsAlgorithmProtectAttributeAsn decoded);
            reader.ThrowIfNotEmpty();
            return decoded;
        }

        public static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out CmsAlgorithmProtectAttributeAsn decoded)
        {
            var sequence = reader.ReadSequence();
            decoded = default;
            AlgorithmIdentifierAsn digestAlgorithm;
            AlgorithmIdentifierAsn.Decode(ref sequence, rebind, out digestAlgorithm);
            decoded.DigestAlgorithm = digestAlgorithm;
            Asn1Tag tag = sequence.PeekTag();
            if (tag.TagValue == 1 && tag.TagClass == TagClass.ContextSpecific)
            {
                AlgorithmIdentifierAsn sigAlg;
                AlgorithmIdentifierAsn.Decode(ref sequence, new Asn1Tag(TagClass.ContextSpecific, 1), rebind, out sigAlg);
                decoded.SignatureAlgorithm = sigAlg;
            }
            else
            {
                AlgorithmIdentifierAsn macAlg;
                AlgorithmIdentifierAsn.Decode(ref sequence, new Asn1Tag(TagClass.ContextSpecific, 2), rebind, out macAlg);
                decoded.MacAlgorithm = macAlg;
            }
        }
    }
}
