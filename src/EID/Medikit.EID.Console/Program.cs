// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;

namespace Medikit.EID.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var discovery = new BeIDCardDiscovery())
            {
                var readers = discovery.GetReaders();
                using (var connection = discovery.Connect(readers.First()))
                {
                    var identity = connection.GetIdentity();
                    var addr = connection.GetAddress();
                    var sigCertificate = connection.GetSignCertificate();
                    var authCertificate = connection.GetAuthCertificate();
                    var rrnCertificate = connection.GetRRNCertificate();
                    var rootCaCertificate = connection.GetRootCACertificate();
                    var citizenCaCertificate = connection.GetCitizenCACertificate();
                }
            }
        }
    }
}
