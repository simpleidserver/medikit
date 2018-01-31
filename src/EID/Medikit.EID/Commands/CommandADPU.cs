// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EID.Helpers;
using System;

namespace Medikit.EID.Commands
{
    public class CommandAPDU
    {
        private const long serialVersionUID = 398698301286670877;
        private byte[] _apdu;
        private int _nc;
        private int _ne;
        private int _dataOffset;

        public CommandAPDU(byte[] apdu)
        {
            _apdu = (byte[])apdu.Clone();
            Parse();
        }

        public CommandAPDU(byte[] apdu, int apduOffset, int apduLength)
        {
            CommandAPDU commandApdu = this;
            CheckArrayBounds(apdu, apduOffset, apduLength);
            _apdu = new byte[apduLength];
            ByteCodeHelper.ArrayCopy(apdu, apduOffset, _apdu, 0, apduLength);
            Parse();
        }

        public CommandAPDU(int cla, int ins, int p1, int p2) : this(cla, ins, p1, p2, null, 0, 0, 0)
        {
        }

        public CommandAPDU(int cla, int ins, int p1, int p2, int ne) : this(cla, ins, p1, p2, null, 0, 0, ne)
        {
        }

        public CommandAPDU(int cla, int ins, int p1, int p2, byte[] data) : this(cla, ins, p1, p2, data, 0, GetArrayLength(data), 0)
        {
        }

        public CommandAPDU(int cla, int ins, int p1, int p2, byte[] data, int dataOffset, int dataLength, int ne)
        {
            CommandAPDU commandApdu = this;
            CheckArrayBounds(data, dataOffset, dataLength);
            if (dataLength > (int)ushort.MaxValue)
            {
                throw new ArgumentException("DataLength is too large");
            }
            if (ne < 0)
            {
                throw new ArgumentException("ne must not be negative");
            }
            if (ne > 65536)
            {
                throw new ArgumentException("ne is too large");
            }

            _nc = dataLength;
            if (dataLength == 0)
            {
                if (ne == 0)
                {
                    _apdu = new byte[4];
                    SetHeader(cla, ins, p1, p2);
                }
                else if (ne <= 256)
                {
                    int num = ne == 256 ? 0 : (int)(sbyte)ne;
                    _apdu = new byte[5];
                    SetHeader(cla, ins, p1, p2);
                    _apdu[4] = (byte)num;
                }
                else
                {
                    int num1;
                    int num2;
                    if (ne == 65536)
                    {
                        num1 = 0;
                        num2 = 0;
                    }
                    else
                    {
                        num1 = (int)(sbyte)(ne >> 8);
                        num2 = (int)(sbyte)ne;
                    }

                    _apdu = new byte[7];
                    SetHeader(cla, ins, p1, p2);
                    _apdu[5] = (byte)num1;
                    _apdu[6] = (byte)num2;
                }
            }
            else if (ne == 0)
            {
                if (dataLength <= (int)byte.MaxValue)
                {
                    _apdu = new byte[5 + dataLength];
                    SetHeader(cla, ins, p1, p2);
                    _apdu[4] = (byte)dataLength;
                    _dataOffset = 5;
                    ByteCodeHelper.ArrayCopy(data, dataOffset, _apdu, 5, dataLength);
                }
                else
                {
                    _apdu = new byte[7 + dataLength];
                    SetHeader(cla, ins, p1, p2);
                    _apdu[4] = (byte)0;
                    _apdu[5] = (byte)(dataLength >> 8);
                    _apdu[6] = (byte)dataLength;
                    _dataOffset = 7;
                    ByteCodeHelper.ArrayCopy(data, dataOffset, _apdu, 7, dataLength);
                }
            }
            else if (dataLength <= (int)byte.MaxValue && ne <= 256)
            {
                _apdu = new byte[6 + dataLength];
                SetHeader(cla, ins, p1, p2);
                _apdu[4] = (byte)dataLength;
                _dataOffset = 5;
                ByteCodeHelper.ArrayCopy(data, dataOffset, _apdu, 5, dataLength);
                _apdu[_apdu.Length - 1] = ne == 256 ? (byte)0 : (byte)ne;
            }
            else
            {
                _apdu = new byte[9 + dataLength];
                SetHeader(cla, ins, p1, p2);
                _apdu[4] = (byte)0;
                _apdu[5] = (byte)(dataLength >> 8);
                _apdu[6] = (byte)dataLength;
                _dataOffset = 7;
                ByteCodeHelper.ArrayCopy(data, dataOffset, _apdu, 7, dataLength);
                if (ne == 65536)
                {
                    return;
                }

                int index = _apdu.Length - 2;
                _apdu[index] = (byte)(ne >> 8);
                _apdu[index + 1] = (byte)ne;
            }
        }

        public byte[] Adpu
        {
            get { return _apdu; }
        }

        private void SetHeader(int obj0, int obj1, int obj2, int obj3)
        {
            _apdu[0] = (byte)obj0;
            _apdu[1] = (byte)obj1;
            _apdu[2] = (byte)obj2;
            _apdu[3] = (byte)obj3;
        }

        private void CheckArrayBounds(byte[] obj0, int obj1, int obj2)
        {
            if (obj1 < 0 || obj2 < 0)
            {
                throw new ArgumentException("Offset and length must not be negative");
            }
            if (obj0 == null)
            {
                if (obj1 != 0 && obj2 != 0)
                {
                    throw new ArgumentException("offset and length must be 0 if array is null");
                }
            }
            else if (obj1 > obj0.Length - obj2)
            {
                throw new ArgumentException("Offset plus length exceed array size");
            }
        }

        private void Parse()
        {
            if (_apdu.Length < 4)
            {
                throw new ArgumentException("apdu must be at least 4 bytes long");
            }

            if (_apdu.Length == 4) { return; }
            int num1 = _apdu[4];
            if (_apdu.Length == 5) { _ne = num1 != 0 ? num1 : 256; }
            else if (num1 != 0)
            {
                if (_apdu.Length == 5 + num1)
                {
                    _nc = num1;
                    _dataOffset = 5;
                }
                else if (_apdu.Length == 6 + num1)
                {
                    _nc = num1;
                    _dataOffset = 5;
                    int num2 = (int)_apdu[_apdu.Length - 1];
                    _ne = num2 != 0 ? num2 : 256;
                }
                else
                {
                    throw new ArgumentException("Invalid APDU: length");
                }
            }
            else
            {
                if (_apdu.Length < 7)
                {
                    throw new ArgumentException("Invalid APDU: length");
                }
                int num2 = (int)_apdu[5] << 8 | (int)_apdu[6];
                if (_apdu.Length == 7)
                {
                    _ne = num2 != 0 ? num2 : 65536;
                }
                else
                {
                    if (num2 == 0)
                    {
                        throw new ArgumentException("Invalid APDU: length");
                    }
                    if (_apdu.Length == 7 + num2)
                    {
                        _nc = num2;
                        _dataOffset = 7;
                    }
                    else if (_apdu.Length == 9 + num2)
                    {
                        _nc = num2;
                        _dataOffset = 7;
                        int index = _apdu.Length - 2;
                        int num3 = (int)_apdu[index] << 8 | (int)_apdu[index + 1];
                        _ne = num3 != 0 ? num3 : 65536;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid APDU: length");
                    }
                }
            }
        }

        private static int GetArrayLength(byte[] arr)
        {
            if (arr != null) { return arr.Length; }
            return 0;
        }
    }
}
