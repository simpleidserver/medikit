// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.EID.Helpers
{
    internal static class ByteCodeHelper
    {
        public static void ArrayCopy(Array src, int srcStart, Array dest, int destStart, int len)
        {
            Buffer.BlockCopy(src, srcStart, dest, destStart, len);
        }
    }
}
