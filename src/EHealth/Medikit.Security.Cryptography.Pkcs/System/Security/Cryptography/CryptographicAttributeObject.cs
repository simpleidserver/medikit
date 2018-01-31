// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Medikit.Security.Cryptography.Pkcs.Resources;
using System;
using System.Security.Cryptography;

namespace Medikit.Security.Cryptography
{
    public sealed class CryptographicAttributeObject
    {
        //
        // Constructors.
        //
        public CryptographicAttributeObject(Oid oid)
            : this(oid, new AsnEncodedDataCollection())
        {
        }

        public CryptographicAttributeObject(Oid oid, AsnEncodedDataCollection? values)
        {
            _oid = new Oid(oid);
            if (values == null)
            {
                Values = new AsnEncodedDataCollection();
            }
            else
            {
                foreach (AsnEncodedData asn in values)
                {
                    if (!string.Equals(asn.Oid!.Value, oid.Value, StringComparison.Ordinal))
                        throw new InvalidOperationException(string.Format(Strings.InvalidOperation_WrongOidInAsnCollection, oid.Value, asn.Oid.Value));
                }
                Values = values;
            }
        }

        //
        // Public properties.
        //

        public Oid Oid
        {
            get
            {
                return new Oid(_oid);
            }
        }

        public AsnEncodedDataCollection Values { get; }
        private readonly Oid _oid;
    }
}
