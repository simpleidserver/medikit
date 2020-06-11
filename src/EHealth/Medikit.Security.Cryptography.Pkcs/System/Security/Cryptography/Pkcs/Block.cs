// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.Security.Cryptography.Pkcs
{
    public class Block
    {
        byte[] _b = new byte[8];
        public Block(Block b) : this(b.Bytes) { }
        public Block(byte[] bytes) : this(bytes, 0) { }
        public Block(byte[] bytes, int index)
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");
            if (index + 8 > bytes.Length)
                throw new ArgumentException("Buffer length error");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index");

            Array.Copy(bytes, index, _b, 0, 8);
        }

        public byte[] Bytes
        {
            get { return _b; }
        }

        public byte[] Concat(Block right)
        {
            if (right == null)
                throw new ArgumentNullException("right");

            byte[] output = new byte[16];

            _b.CopyTo(output, 0);
            right.Bytes.CopyTo(output, 8);

            return output;
        }

        public static Block[] BytesToBlocks(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");
            if (bytes.Length % 8 != 0)
                throw new ArgumentException("Disible by 0");

            Block[] blocks = new Block[bytes.Length / 8];
            for (int i = 0; i < bytes.Length; i += 8)
                blocks[i / 8] = new Block(bytes, i);

            return blocks;
        }

        public static byte[] BlocksToBytes(Block[] blocks)
        {
            if (blocks == null)
                throw new ArgumentNullException("blocks");

            byte[] bytes = new byte[blocks.Length * 8];

            for (int i = 0; i < blocks.Length; i++)
                blocks[i].Bytes.CopyTo(bytes, i * 8);

            return bytes;
        }

        public static Block operator ^(Block left, long right)
        {
            return Xor(left, right);
        }

        public static Block Xor(Block left, long right)
        {
            if (left == null)
                throw new ArgumentNullException("left");

            Block result = new Block(left);
            ReverseBytes(result.Bytes);
            long temp = BitConverter.ToInt64(result.Bytes, 0);

            result = new Block(BitConverter.GetBytes(temp ^ right));
            ReverseBytes(result.Bytes);
            return result;
        }

        internal static void ReverseBytes(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length / 2; i++)
            {
                byte temp = bytes[i];
                bytes[i] = bytes[(bytes.Length - 1) - i];
                bytes[(bytes.Length - 1) - i] = temp;
            }
        }
    }
}
