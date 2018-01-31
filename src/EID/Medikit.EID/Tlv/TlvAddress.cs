// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Medikit.EID.Tlv
{
    public class TlvAddress
    {
        [TlvField(1)]
        public string StreetAndNumber { get; set; }
        [TlvField(2)]
        public string Zip { get; set; }
        [TlvField(3)]
        public string Municipality { get; set; }
    }
}
