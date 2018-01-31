// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.EID
{
    public class BeIDDigest
    {
        private readonly string _name;
        private readonly int _id;
        private readonly byte[] _prefix;
        private readonly byte _algorithmReference;

        private BeIDDigest(string name, int id)
        {
            _name = name;
            _id = id;
        }

        private BeIDDigest(string name, int id, byte[] prefix, byte algorithmReference) : this(name, id)
        {
            _prefix = prefix;
            _algorithmReference = algorithmReference;
        }

        private BeIDDigest(string name, int id, byte[] prefix) : this(name, id, prefix, 1) { }

        public byte AlgorithmReference
        {
            get { return _algorithmReference; }
        }

        public static BeIDDigest PlainText = new BeIDDigest("PLAIN_TEXT", 0, new byte[]
        {
            48,
            byte.MaxValue,
            48,
            9,
            6,
            7,
            96,
            56,
            1,
            2,
            1,
            3,
            1,
            4,
            byte.MaxValue
        });
        public static BeIDDigest Sha1 = new BeIDDigest("SHA_1", 1, new byte[]
        {
            48,
            33,
            48,
            9,
            6,
            5,
            43,
            14,
            3,
            2,
            26,
            5,
            0,
            4,
            20
        });
        public static BeIDDigest Sha224 = new BeIDDigest("SHA_224", 2, new byte[]
        {
            48,
            45,
            48,
            13,
            6,
            9,
            96,
            134,
            72,
            1,
            101,
            3,
            4,
            2,
            4,
            5,
            0,
            4,
            28
        });
        public static BeIDDigest Sha256 = new BeIDDigest("SHA_256", 3, new byte[]
        {
            48,
            49,
            48,
            13,
            6,
            9,
            96,// 96,
            134,
            72,
            1,
            101,
            3,
            4,
            2,
            1,
            5,
            0,
            4,
            32
        });
        public static BeIDDigest Sha384 = new BeIDDigest("SHA_384", 4, new byte[]
        {
            48,
            65,
            48,
            13,
            6,
            9,
            96,
            134,
            72,
            1,
            101,
            3,
            4,
            2,
            20,
            5,
            0,
            4,
            48
        });
        public static BeIDDigest Sha512 = new BeIDDigest("SHA_512", 5, new byte[]
        {
            48,
            81,
            48,
            13,
            6,
            9,
            96,
            134,
            72,
            1,
            101,
            3,
            4,
            2,
            3,
            5,
            0,
            4,
            64
        });
        public static BeIDDigest Ripemd128 = new BeIDDigest("RIPEMD_128", 6, new byte[]
        {
            48,
            29,
            48,
            9,
            6,
            5,
            43,
            36,
            3,
            2,
            2,
            5,
            0,
            4,
            16
        });
        public static BeIDDigest Ripemd160 = new BeIDDigest("RIPEMD_160", 7, new byte[]
        {
            48,
            33,
            48,
            9,
            6,
            5,
            43,
            36,
            3,
            2,
            1,
            5,
            0,
            4,
            20
        });
        public static BeIDDigest Ripemd256 = new BeIDDigest("RIPEMD_256", 8, new byte[]
        {
            48,
            45,
            48,
            9,
            6,
            5,
            43,
            36,
            3,
            2,
            3,
            5,
            0,
            4,
            32
        });

        public virtual byte[] GetPrefix(int valueLength)
        {
            var numArray = new byte[_prefix.Length];
            Array.Copy(_prefix, numArray, _prefix.Length);
            // numArray[1] = (byte)(valueLength + 13);
            // numArray[14] = (byte)valueLength;
            return numArray;
        }
    }
}
