// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EID.Exceptions;
using Medikit.EID.Helpers;
using Medikit.EID.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Medikit.EID
{
    public class ResponseAPDU
    {
        private static Dictionary<byte[], Func<string>> MAPPING_ADPU_RESPONSE_TO_ERROR = new Dictionary<byte[], Func<string>>
        {
            { new byte[] { 98, 131 }, () => Global.SelectedFileNotActivated }, // 62 83
            { new byte[] { 100, 131 }, () => Global.NoPreciseDiagnostic }, // 64 00
            { new byte[] { 101, 129 }, () => Global.EEPromCorrupted }, // 65 81
            { new byte[] { 106, 130 }, () => Global.FileNotFound }, // 6A 82
            { new byte[] { 106, 134 }, () => Global.WrongParameterP1P2 }, // 6A 86
            { new byte[] { 106, 135 }, () => Global.LCInconsistentP1P2 }, // 6A 87 
            { new byte[] { 109, 0 }, () => Global.CommandNotAvailable }, // 6D 00
            { new byte[] { 110, 0 }, () => Global.CLANotSupported } // 6E 00
        };

        private readonly byte[] _apdu;

        public ResponseAPDU(byte[] apdu)
        {
            Check(apdu);
            _apdu = apdu;
        }

        public byte[] GetApdu()
        {
            return _apdu;
        }

        public int GetSW1()
        {
            return _apdu[_apdu.Length - 2];
        }

        public int GetSW2()
        {
            return _apdu[_apdu.Length - 1];
        }

        public int GetSw()
        {
            return GetSW1() << 8 | GetSW2();
        }

        public int GetNr()
        {
            return _apdu.Length - 2;
        }

        public byte[] GetData()
        {
            byte[] numArray = new byte[_apdu.Length - 2];
            ByteCodeHelper.ArrayCopy(_apdu, 0, numArray, 0, numArray.Length);
            return numArray;
        }

        private static void Check(byte[] apdu)
        {
            var status = apdu.Skip(apdu.Count() - 2).Take(2);
            var rec = MAPPING_ADPU_RESPONSE_TO_ERROR.FirstOrDefault(kvp => kvp.Key.SequenceEqual(status));
            if (!rec.Equals(default(KeyValuePair<byte[], string>)) && rec.Value != null)
            {
                throw new BeIDCardException(rec.Value());
            }

            if (apdu.First() == 0x6C)
            {
                throw new WrongLengthException(apdu[1]);
            }
        }
    }
}
