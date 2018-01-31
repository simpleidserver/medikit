// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.EHealth.KeyStore
{
    public class KeyStoreReader
    {
        public async Task<KeyStoreFile> Load(string fileName, CancellationToken token = default(CancellationToken))
        {
            var keyStoreFile = new KeyStoreFile();
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var l = fs.Length;
                var numberOfEntriesPayload = new byte[4];
                var magicNumber = await GetInt32(fs, token);
                keyStoreFile.VersionNumber = await GetInt32(fs, token);
                var numberOfEntries = await GetInt32(fs, token);
                for (var currentEntry = 0; currentEntry < numberOfEntries; currentEntry++)
                {
                    var entryType = await GetInt32(fs, token);
                    var utf = await GetUTF8(fs, token);
                    var creationDateTime = (await GetInt64(fs, token)).ToDateTime();
                    switch (entryType)
                    {
                        case 1:
                            // TODO
                            break;
                        case 2:
                            var certificateType = await GetUTF8(fs, token);
                            var encoded = await GetEncoded(fs, token);
                            var certificate = new X509Certificate2(encoded);
                            keyStoreFile.Certificates.Add(new KeyStoreCertificate
                            {
                                CreateDateTime = creationDateTime,
                                Name = utf,
                                Type = certificateType,
                                Certificate = certificate
                            });
                            break;
                    }
                }
            }

            return keyStoreFile;
        }

        private static async Task<byte[]> GetEncoded(FileStream stream, CancellationToken token)
        {
            var length = await GetInt32(stream, token);
            var payload = new byte[length];
            await stream.ReadAsync(payload, 0, payload.Length, token);
            return payload;
        }

        private static async Task<string> GetUTF8(FileStream stream, CancellationToken token)
        {
            var utfLength = await GetInt16(stream, token);
            var utfPayload = new byte[utfLength];
            await stream.ReadAsync(utfPayload, 0, utfLength, token);
            return Encoding.UTF8.GetString(utfPayload);
        }

        private static async Task<Int64> GetInt64(FileStream stream, CancellationToken token)
        {
            var payload = new byte[8];
            await stream.ReadAsync(payload, 0, payload.Length, token);
            Array.Reverse(payload, 0, 8);
            return BitConverter.ToInt64(payload, 0);
        }

        private static async Task<int> GetInt32(FileStream stream, CancellationToken token)
        {
            var payload = new byte[4];
            await stream.ReadAsync(payload, 0, payload.Length, token);
            Array.Reverse(payload, 0, 4);
            return BitConverter.ToInt32(payload, 0);
        }

        private static async Task<ushort> GetInt16(FileStream stream, CancellationToken token)
        {
            var payload = new byte[2];
            await stream.ReadAsync(payload, 0, payload.Length, token);
            Array.Reverse(payload, 0, 2);
            return BitConverter.ToUInt16(payload, 0);
        }

        private static byte[] GetPreKeyedHash(char[] password)
        {
            int i, j;
            byte[] passwdBytes = new byte[password.Length * 2];
            for (i = 0, j = 0; i < password.Length; i++)
            {
                passwdBytes[j++] = (byte)(password[i] >> 8);
                passwdBytes[j++] = (byte)password[i];
            }

            for (i = 0; i < passwdBytes.Length; i++)
                passwdBytes[i] = 0;

            var lst = new List<byte>();
            lst.AddRange(passwdBytes);
            lst.AddRange(Encoding.UTF8.GetBytes("Mighty Aphrodite"));
            return SHA1.Create().ComputeHash(lst.ToArray());
        }
    }
}
