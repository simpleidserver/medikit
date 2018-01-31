// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EID.Commands;
using Medikit.EID.Exceptions;
using Medikit.EID.Resources;
using Medikit.EID.Tlv;
using PcscDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Medikit.EID
{
    public class BeIDCardConnector : IDisposable
    {
        private static int BLOCK_SIZE = 256;
        private static int BUFFER_SIZE = 3000;
        private readonly PcscConnection _connection;
        private readonly PcscContext _context;

        internal BeIDCardConnector(PcscConnection connection, PcscContext context) 
        {
            _connection = connection;
            _context = context;
        }

        public void Disconnect()
        {
            if (_connection != null)
            {
                if (_connection.IsConnect) { _connection.Disconnect(); }
                _connection.Dispose();
            }
        }

        public TlvIdentity GetIdentity()
        {
            var payload = ReadFile(FileType.Identity);
            var parser = new TlvParser();
            return parser.Parse<TlvIdentity>(payload);
        }

        public TlvAddress GetAddress()
        {
            var payload = ReadFile(FileType.Address);
            var parser = new TlvParser();
            return parser.Parse<TlvAddress>(payload);
        }

        public List<X509Certificate> GetAuthCertificateChain()
        {
            var result = new List<X509Certificate>();
            result.Add(GetAuthCertificate());
            result.Add(GetCitizenCACertificate());
            result.Add(GetRootCACertificate());
            return result;
        }

        public List<X509Certificate> GetSigningCertificateChain()
        {
            var result = new List<X509Certificate>();
            result.Add(GetSignCertificate());
            result.Add(GetCitizenCACertificate());
            result.Add(GetRootCACertificate());
            return result;
        }

        public X509Certificate GetSignCertificate()
        {
            var signingCertificate = ReadFile(FileType.NonRepudiationCertificate);
            return new X509Certificate(signingCertificate);
        }

        public X509Certificate GetAuthCertificate()
        {
            var authenticateCertificate = ReadFile(FileType.AuthentificationCertificate);
            return new X509Certificate(authenticateCertificate);
        }

        public X509Certificate GetRRNCertificate()
        {
            var rrnCertificate = ReadFile(FileType.RRNCertificate);
            return new X509Certificate(rrnCertificate);
        }

        public X509Certificate GetRootCACertificate()
        {
            var rootCACertificate = ReadFile(FileType.RootCertificate);
            return new X509Certificate(rootCACertificate);
        }

        public X509Certificate GetCitizenCACertificate()
        {
            var citizenCACertificate = ReadFile(FileType.CACertificate);
            return new X509Certificate(citizenCACertificate);
        }

        public byte[] GetPhoto()
        {
            return ReadFile(FileType.Photo);
        }

        public byte[] SignWithAuthenticationCertificate(byte[] digestValue, BeIDDigest beIDDigest, string pin)
        {
            if (digestValue == null)
            {
                throw new ArgumentNullException(nameof(digestValue));
            }

            if (beIDDigest == null)
            {
                throw new ArgumentNullException(nameof(beIDDigest));
            }

            if (string.IsNullOrWhiteSpace(pin))
            {
                throw new ArgumentNullException(nameof(pin));
            }

            var fileType = FileType.AuthentificationCertificate;
            byte[] result = null;
            BeginExclusive();
            try
            {
                unchecked
                {                    
                    var algReference = beIDDigest.AlgorithmReference;
                    var responseApdu = SendCommand(BeIDCommandAPDU.SELECT_ALGORITHM_AND_PRIVATE_KEY, new byte[] // Select the key & algorithm.
                    {
                        4,
                        128,
                        algReference,
                        132,
                        fileType.KeyId
                    });
                    if (responseApdu.GetSw() != 0x9000)
                    {
                        throw new BeIDCardException(Global.SelectPrivateKeyError);
                    }

                    var verifyPinResponse = VerifyPin(pin);
                    var data = new List<byte>();
                    data.AddRange(beIDDigest.GetPrefix(digestValue.Length));
                    data.AddRange(digestValue);
                    var signatureResult = SendCommand(BeIDCommandAPDU.COMPUTE_DIGITAL_SIGNATURE, data.ToArray());
                    var getResponse = GetResponse(BLOCK_SIZE);
                    result = getResponse.GetData();
                }
            }
            finally
            {
                EndExclusive();
            }

            return result;
        }
        
        public bool VerifyPin(string pin)
        {
            if (string.IsNullOrWhiteSpace(pin))
            {
                throw new ArgumentNullException(nameof(pin));
            }

            var data = new byte[]
            {
                    (byte)(32 | pin.Length),
                    byte.MaxValue,
                    byte.MaxValue,
                    byte.MaxValue,
                    byte.MaxValue,
                    byte.MaxValue,
                    byte.MaxValue,
                    byte.MaxValue
            };
            int index = 0;
            while (index < pin.Length)
            {
                int num2 = (int)(sbyte)((pin[index] - 48 << 4) + ((index + 1 >= pin.Length ? 63 : pin[index + 1]) - 48));
                data[index / 2 + 1] = (byte)num2;
                index += 2;
            }

            var sendResponse = SendCommand(BeIDCommandAPDU.VERIFY_PIN, data);
            int sw = sendResponse.GetSw();
            return sw == 0x9000;
        }

        public void Dispose()
        {
            Disconnect();
        }

        private byte[] ReadFile(FileType file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            CheckConnection();
            SendCommand(BeIDCommandAPDU.SELECT_FILE, file.FileId);
            return ReadBinary();
        }

        private byte[] ReadBinary()
        {
            var blockSize = BLOCK_SIZE;
            var result = new List<byte>();
            int offset = 0;
            byte[] data = new byte[BLOCK_SIZE];
            do
            {
                try
                {
                    var readBinaryAdpu = SendCommand(BeIDCommandAPDU.READ_BINARY, offset >> 8, offset & 0xFF, blockSize);
                    int sw = readBinaryAdpu.GetSw();
                    if (sw == 0x6B00)
                    {
                        break;
                    }

                    if (sw != 0x9000)
                    {
                        throw new BeIDCardException(string.Format(Global.AdpuResponseError, sw));
                    }

                    data = readBinaryAdpu.GetData();
                    result.AddRange(data);
                    offset += data.Count();
                }
                catch(WrongLengthException ex)
                {
                    blockSize = ex.Length;
                }
            }
            while (BLOCK_SIZE == data.Count());
            return result.ToArray();
        }

        private ResponseAPDU GetResponse(int length)
        {
            try
            {
                return SendCommand(BeIDCommandAPDU.GET_RESPONSE, 0, 0, length);
            }
            catch(WrongLengthException ex)
            {
                return GetResponse(ex.Length);
            }
        }

        private void BeginExclusive()
        {
            var error = _context.Provider.SCardBeginTransaction(_connection.Handle);
            if (error != SCardError.Successs)
            {
                throw new BeIDCardException(Global.CannotStartTransaction);
            }
        }

        private void EndExclusive()
        {
            _context.Provider.SCardEndTransaction(_connection.Handle, SCardDisposition.Leave);
        }

        private ResponseAPDU SendCommand(BeIDCommandAPDU command, byte[] data)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var commandAdpu = new CommandAPDU(command.Cla, command.Ins, command.P1, command.P2, data);
            return Transmit(commandAdpu, BUFFER_SIZE);
        }

        private ResponseAPDU SendCommand(BeIDCommandAPDU command, int p1, int p2, int le)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var commandAdpu = new CommandAPDU(command.Cla, command.Ins, p1, p2, le);
            return Transmit(commandAdpu, BUFFER_SIZE);
        }

        private ResponseAPDU Transmit(CommandAPDU commandAdpu, int bufferSize)
        {
            if (commandAdpu == null)
            {
                throw new ArgumentNullException(nameof(commandAdpu));
            }

            var result = _connection.Transmit(commandAdpu.Adpu, bufferSize);
            return new ResponseAPDU(result);
        }

        private void CheckConnection()
        {
            if (_connection == null || !_connection.IsConnect)
            {
                throw new BeIDCardException(Global.NoEstablishedConnection);
            }
        }
    }
}
