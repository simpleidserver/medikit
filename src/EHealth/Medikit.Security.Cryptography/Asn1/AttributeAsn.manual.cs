// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable enable

using System;
using System.Security.Cryptography;

namespace Medikit.Security.Cryptography.Asn1
{
    public partial struct AttributeAsn
    {
        public AttributeAsn(AsnEncodedData attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            AttrType = new Oid(attribute.Oid!);
            AttrValues = new[] { new ReadOnlyMemory<byte>(attribute.RawData) };
        }
    }
}
