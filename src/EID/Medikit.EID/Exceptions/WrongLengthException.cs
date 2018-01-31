// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Medikit.EID.Exceptions
{
    public class WrongLengthException : BeIDCardException
    {
        public WrongLengthException(int length) : base(string.Empty)
        {
            Length = length;
        }

        public int Length { get; set; }
    }
}
