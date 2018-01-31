// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Medikit.EID.Commands
{
    /// <summary>
    /// Commands are described in http://www.foo.be/eID/opensc-belgium/BEID-CardSpecs-v2.0.0.pdf.
    /// </summary>
    internal class BeIDCommandAPDU
    {
        public static BeIDCommandAPDU SELECT_APPLET_0 = new BeIDCommandAPDU("SELECT_APPLET_0", 0, 0, 164, 4, 12);
        public static BeIDCommandAPDU SELECT_APPLET_1 = new BeIDCommandAPDU("SELECT_APPLET_1", 1, 0, 164, 4, 12);
        /// <summary>
        /// Used to select a file.
        /// </summary>
        public static BeIDCommandAPDU SELECT_FILE = new BeIDCommandAPDU("SELECT_FILE", 2, 0, 164, 8, 12);
        public static BeIDCommandAPDU READ_BINARY = new BeIDCommandAPDU("READ_BINARY", 3, 0, 176);
        public static BeIDCommandAPDU VERIFY_PIN = new BeIDCommandAPDU("VERIFY_PIN", 4, 0, 32, 0, 1);
        public static BeIDCommandAPDU CHANGE_PIN = new BeIDCommandAPDU("CHANGE_PIN", 5, 0, 36, 0, 1);
        public static BeIDCommandAPDU SELECT_ALGORITHM_AND_PRIVATE_KEY = new BeIDCommandAPDU("SELECT_ALGORITHM_AND_PRIVATE_KEY", 6, 0, 34, 65, 182);
        /// <summary>
        /// Compute Digital Signature initiates the computation of a digital signature.
        /// The private key and algorithm to be used has been previously defined by the MSE.
        /// </summary>
        public static BeIDCommandAPDU COMPUTE_DIGITAL_SIGNATURE = new BeIDCommandAPDU("COMPUTE_DIGITAL_SIGNATURE", 7, 0, 42, 158, 154);
        public static BeIDCommandAPDU RESET_PIN = new BeIDCommandAPDU("RESET_PIN", 8, 0, 44, 0, 1);
        public static BeIDCommandAPDU GET_CHALLENGE = new BeIDCommandAPDU("GET_CHALLENGE", 9, 0, 132, 0, 0);
        public static BeIDCommandAPDU GET_CARD_DATA = new BeIDCommandAPDU("GET_CARD_DATA", 10, 128, 228, 0, 0);
        public static BeIDCommandAPDU PPDU = new BeIDCommandAPDU("PPDU", 11, (int)byte.MaxValue, 194, 1);
        public static BeIDCommandAPDU GET_RESPONSE = new BeIDCommandAPDU("GET_RESPONSE", 12, 0, 192, 0, 0);

        private BeIDCommandAPDU(string name, int id)
        {
            Name = name;
            Id = id;
            P1 = -1;
            P2 = -1;
        }

        private BeIDCommandAPDU(string name, int id, int cla, int ins) : this(name, id)
        {
            Cla = cla;
            Ins = ins;
        }

        private BeIDCommandAPDU(string name, int id, int cla, int ins, int p1) : this(name, id, cla, ins)
        {
            P1 = p1;
        }

        private BeIDCommandAPDU(string name, int id, int cla, int ins, int p1, int p2) : this(name, id, cla, ins, p1)
        {
            P2 = p2;
        }

        public string Name { get; private set; }
        public int Id { get; private set; }
        public int Cla { get; private set; }
        public int Ins { get; private set; }
        public int P1 { get; private set; }
        public int P2 { get; private set; }
    }
}
