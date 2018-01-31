// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Buffers;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;

namespace Medikit.Security.Cryptography.Asn1
{
    public sealed partial class AsnWriter
    {
        /// <summary>
        ///   Write an Object Identifier with a specified tag.
        /// </summary>
        /// <param name="oid">The object identifier to write.</param>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="oid"/> is <c>null</c>
        /// </exception>
        /// <exception cref="CryptographicException">
        ///   <paramref name="oid"/>.<see cref="Oid.Value"/> is not a valid dotted decimal
        ///   object identifier
        /// </exception>
        /// <exception cref="ObjectDisposedException">The writer has been Disposed.</exception>
        public void WriteRelativeObjectIdentifier(Oid oid)
        {
            WriteTag(new Asn1Tag(TagClass.Universal, (int)UniversalTagNumber.RelativeObjectIdentifier));
            WriteObjectIdentifier(oid);
        }
    }
}
