// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Medikit.EID
{
    public class FileType
    {
        public static FileType Identity = new FileType("Identity", 0, new byte[]
        {
            0x3F,
            0x00,
            (byte)0xDF,
            0x01,
            0x40,
            0x31
        }, 179);
        public static FileType IdentitySignature = new FileType("IdentitySignature", 1, new byte[]
        {
            63,
            0,
            223,
            1,
            64,
            50
        }, 128);
        public static FileType Address = new FileType("Address", 2, new byte[]
        {
            63,
            0,
            223,
            1,
            64,
            51

        }, 121);
        public static FileType AddressSignature = new FileType("AddressSignature", 3, new byte[]
        {
            63,
            0,
            223,
            1,
            64,
            52
        }, 128);
        public static FileType Photo = new FileType("Photo", 4, new byte[]
        {
            63,
            0,
            223,
            1,
            64,
            53
        }, 3064);
        public static FileType AuthentificationCertificate = new FileType("AuthentificationCertificate", 5, new byte[]
        {
            63,
            0,
            223,
            0,
            80,
            56
        }, 1900, 130);
        public static FileType NonRepudiationCertificate = new FileType("NonRepudiationCertificate", 6, new byte[]
        {
            63,
            0,
            223,
            0,
            80,
            57
        }, 1900, 131);
        public static FileType CACertificate = new FileType("CACertificate", 7, new byte[]
        {
            63,
            0,
            223,
            0,
            80,
            58
        }, 1044);
        public static FileType RootCertificate = new FileType("RootCertificate", 8, new byte[]
        {
            63,
            0,
            223,
            0,
            80,
            59
        }, 914);
        public static FileType RRNCertificate = new FileType("RRNCertificate", 9, new byte[]
        {
            63,
            0,
            223,
            0,
            80,
            60
        }, 820);

        private FileType(string name, int id)
        {
            Name = name;
            Id = id;
        }

        private FileType(string name, int id, byte[] fileId, int estimatedMaxSize) : this(name, id)
        {
            FileType fileType = this;
            FileId = fileId;
            EstimatedMaxSize = estimatedMaxSize;
        }

        private FileType(string name, int id, byte[] fileId, int estimatedMaxSize, int keyId) : this(name, id, fileId, estimatedMaxSize)
        {
            KeyId = (byte)keyId;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public byte[] FileId { get; private set; }

        public int EstimatedMaxSize { get; private set; }

        public byte KeyId { get; private set; }
    }
}
