
using System;

namespace Medikit.Security.Cryptography.Asn1
{
    public sealed partial class AsnWriter
    {
        public void WriteBerOctetString(ReadOnlyMemory<byte> content)
        {
            var contentArr = content.ToArray();
            int segmentSize = AsnValueReader.MaxCERSegmentSize;
            int sourceIndex = 0;
            WriteByte((byte)36);
            WriteByte((byte)128);
            while (sourceIndex < contentArr.Length)
            {
                var size = Math.Min(contentArr.Length, sourceIndex + segmentSize) - sourceIndex;
                byte[] str = new byte[size];
                Array.Copy(contentArr, sourceIndex, str, 0, str.Length);
                WriteOctetString(str);
                sourceIndex += segmentSize;
            }

            WriteByte((byte)0);
            WriteByte((byte)0);
        }
    }
}
