// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Medikit.Security.Cryptography;
using Medikit.Security.Cryptography.Asn1;
using Medikit.Security.Cryptography.Asn1.Pkcs7;
using Medikit.Security.Cryptography.Pkcs.Resources;
using System.Security.Cryptography;

namespace Internal.Cryptography.Pal.AnyOS
{
    internal sealed partial class ManagedPkcsPal : PkcsPal
    {
        public override Oid GetEncodedMessageType(byte[] encodedMessage)
        {
            AsnValueReader reader = new AsnValueReader(encodedMessage, AsnEncodingRules.BER);

            ContentInfoAsn.Decode(ref reader, encodedMessage, out ContentInfoAsn contentInfo);

            switch (contentInfo.ContentType)
            {
                case Oids.Pkcs7Data:
                case Oids.Pkcs7Signed:
                case Oids.Pkcs7Enveloped:
                case Oids.Pkcs7SignedEnveloped:
                case Oids.Pkcs7Hashed:
                case Oids.Pkcs7Encrypted:
                    return new Oid(contentInfo.ContentType);
            }

            throw new CryptographicException(Strings.Cryptography_Cms_InvalidMessageType);
        }
    }
}
