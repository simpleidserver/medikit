// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Medikit.Security.Cryptography.Pkcs
{
    public class SigningPolicy
    {
        public static SigningPolicy EHEALTH_CERT = new SigningPolicy("EHEALTH_CERT", 0, 2048, "2.16.840.1.101.3.4.2.1", "BQA=", "RSASSA-PSS", "1.2.840.113549.1.1.10", "MDSgDzANBglghkgBZQMEAgEFAKEcMBoGCSqGSIb3DQEBCDANBglghkgBZQMEAgEFAKIDAgEg", new List<string> { });
        public static SigningPolicy EID_CERT = new SigningPolicy("EID_CERT", 1, 1024, "2.16.840.1.101.3.4.2.1", "BQA=", "SHA256withRSA", "1.2.840.113549.1.1.11", "BQA=", new List<string> { "1.2.840.113549.1.1.1" });

        public SigningPolicy(string name, int value, int signKeySize, string digestAlgorithmOID, string digestAlgorithmParameters, string signatureAlgorithmName, string encryptionAlgorithmOID, string base64Parameters, List<string> altEncryptionAlgorithmOIDs)
        {
            Name = name;
            Value = value;
            SignKeySize = signKeySize;
            DigestAlgorithmOID = digestAlgorithmOID;
            DigestAlgorithmParameters = Convert.FromBase64String(digestAlgorithmParameters);
            SignatureAlgorithmName = signatureAlgorithmName;
            EncryptionAlgorithmOID = encryptionAlgorithmOID;
            SigningParameters = Convert.FromBase64String(base64Parameters);
            AltEncryptionAlgorithmOIDs = altEncryptionAlgorithmOIDs;
        }

        public string Name { get; private set; }
        public int Value { get; private set; }
        public int SignKeySize { get; private set; }
        public string DigestAlgorithmOID { get; private set; }
        public byte[] DigestAlgorithmParameters { get; private set; }
        public string SignatureAlgorithmName { get; private set; }
        public string EncryptionAlgorithmOID { get; private set; }
        public byte[] SigningParameters { get; private set; }
        public List<string> AltEncryptionAlgorithmOIDs { get; private set; }
    }
}
