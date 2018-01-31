// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.EID.Tlv
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TlvFieldAttribute : Attribute
    {
        public TlvFieldAttribute(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }
}