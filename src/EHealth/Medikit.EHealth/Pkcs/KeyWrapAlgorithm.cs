// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.IO;
using System.Security.Cryptography;

namespace Medikit.EHealth.Pkcs
{
    public class KeyWrapAlgorithm
    {
        private static byte[] DefaultIV = { 0xA6, 0xA6, 0xA6, 0xA6, 0xA6, 0xA6, 0xA6, 0xA6 };
        private readonly byte[] _kek;

        public KeyWrapAlgorithm(byte[] kek)
        {
            ValidateKEK(kek);
            _kek = kek;
        }

        public byte[] WrapKey(byte[] plaintext)
        {
            ValidateInput(plaintext, "plaintext");

            // 1) Initialize variables

            Block A = new Block(DefaultIV);
            Block[] R = Block.BytesToBlocks(plaintext);
            long n = R.Length;

            // 2) Calculate intermediate values

            for (long j = 0; j < 6; j++)
            {
                for (long i = 0; i < n; i++)
                {
                    long t = n * j + i + 1;
                    Block[] B = Encrypt(A.Concat(R[i]));
                    A = MSB(B);
                    R[i] = LSB(B);
                    A ^= t;
                }
            }

            // 3) Output the results

            Block[] C = new Block[n + 1];
            C[0] = A;
            for (long i = 1; i <= n; i++)
                C[i] = R[i - 1];

            return Block.BlocksToBytes(C);
        }

        public byte[] UnwrapKey(byte[] ciphertext)
        {
            ValidateInput(ciphertext, "ciphertext");
            Block[] C = Block.BytesToBlocks(ciphertext);

            // 1) Initialize variables

            Block A = C[0];
            Block[] R = new Block[C.Length - 1];
            for (int i = 1; i < C.Length; i++)
                R[i - 1] = C[i];
            long n = R.Length;            
            for (long j = 5; j >= 0; j--)
            {
                for (long i = n - 1; i >= 0; i--)
                {
                    long t = n * j + i + 1;
                    A ^= t;
                    Block[] B = Decrypt(A.Concat(R[i]));
                    A = MSB(B);
                    R[i] = LSB(B);
                }
            }

            if (!ArraysAreEqual(DefaultIV, A.Bytes))
                throw new CryptographicException("Integrity error");

            return Block.BlocksToBytes(R);
        }

        public static byte[] WrapKey(byte[] kek, byte[] plaintext)
        {
            KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(kek);
            return kwa.WrapKey(plaintext);
        }

        public static byte[] UnwrapKey(byte[] kek, byte[] ciphertext)
        {
            KeyWrapAlgorithm kwa = new KeyWrapAlgorithm(kek);
            return kwa.UnwrapKey(ciphertext);
        }

        #region Helper methods

        private static void ValidateKEK(byte[] kek)
        {
            if (kek == null)
                throw new ArgumentNullException("kek");
            if (kek.Length != 16 && kek.Length != 24 && kek.Length != 32)
                throw new ArgumentOutOfRangeException("kek");
        }

        private static void ValidateInput(byte[] input, string paramName)
        {
            if (input == null)
                throw new ArgumentNullException(paramName);
            if (input.Length < 16)
                throw new ArgumentOutOfRangeException(paramName);
            if (input.Length % 8 != 0)
                throw new ArgumentException("Divisible by 0 error");
        }

        private Block[] Encrypt(byte[] plaintext)
        {
            RijndaelManaged alg = new RijndaelManaged();
            alg.Padding = PaddingMode.None;
            alg.Mode = CipherMode.ECB;
            alg.Key = _kek;

            if (plaintext == null)
                throw new ArgumentNullException("plaintext");
            if (plaintext.Length != alg.BlockSize / 8)
                throw new ArgumentOutOfRangeException("plaintext");

            byte[] ciphertext = new byte[alg.BlockSize / 8];

            using (MemoryStream ms = new MemoryStream(plaintext))
            using (ICryptoTransform xf = alg.CreateEncryptor())
            using (CryptoStream cs = new CryptoStream(ms, xf,
                  CryptoStreamMode.Read))
                cs.Read(ciphertext, 0, alg.BlockSize / 8);

            return Block.BytesToBlocks(ciphertext);
        }

        private Block[] Decrypt(byte[] ciphertext)
        {
            RijndaelManaged alg = new RijndaelManaged();
            alg.Padding = PaddingMode.None;
            alg.Mode = CipherMode.ECB;
            alg.Key = _kek;

            if (ciphertext == null)
                throw new ArgumentNullException("ciphertext");
            if (ciphertext.Length != alg.BlockSize / 8)
                throw new ArgumentOutOfRangeException("ciphertext");

            byte[] plaintext;

            using (MemoryStream ms = new MemoryStream())
            using (ICryptoTransform xf = alg.CreateDecryptor())
            using (CryptoStream cs = new CryptoStream(ms, xf,
                  CryptoStreamMode.Write))
            {
                cs.Write(ciphertext, 0, alg.BlockSize / 8);
                plaintext = ms.ToArray();
            }

            return Block.BytesToBlocks(plaintext);
        }

        private static Block MSB(Block[] B)
        {
            return B[0];
        }

        private static Block LSB(Block[] B)
        {
            return B[1];
        }

        private static bool ArraysAreEqual(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
                if (array1[i] != array2[i])
                    return false;
            return true;
        }

        #endregion
    }
}
