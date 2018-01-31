
using System;

namespace Medikit.Security.Cryptography.Asn1
{
    public sealed partial class AsnWriter
    {
        public void WriteTaggedObject(byte tagValue, ReadOnlyMemory<byte> payload)
        {
            PushTaggedObject(tagValue);
            foreach(var r in payload.Span)
            {
                WriteByte(r);
            }

            PopTaggedObject();
        }

        public void PushTaggedObject(byte tagValue)
        {
            WriteByte(tagValue);
            WriteByte(128);
        }

        public void PopTaggedObject()
        {
            WriteByte(0);
            WriteByte(0);
        }
    }
}
