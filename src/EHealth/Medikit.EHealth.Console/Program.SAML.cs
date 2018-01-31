// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SAML;
using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.SOAP.DTOs;
using System.Threading.Tasks;

namespace Medikit.EHealth.Console
{
    public partial class Program
    {

        private static async Task<SOAPEnvelope<SAMLResponseBody>> BuildSTSIdentityRequest()
        {
            var sessionService = (ISessionService)_serviceProvider.GetService(typeof(ISessionService));
            var result = await sessionService.BuildFallbackSession();
            return result;
        }

        /*
        private static async Task<SOAPEnvelope<SAMLResponseBody>> BuildSTSEidRequest()
        {
            SOAPEnvelope<SAMLRequestBody> samlEnv = null;
            using (var discovery = new BeIDCardDiscovery())
            {
                var readers = discovery.GetReaders();
                using (var connection = discovery.Connect(readers.First()))
                {
                    var certificate = connection.GetAuthCertificate();
                    var authCertificate = new X509Certificate2(connection.GetAuthCertificate().Export(X509ContentType.Cert));
                    samlEnv = new STSRequestBuilder(authCertificate)
                        .BuildPerson((payload) =>
                        {
                            byte[] hashPayload = null;
                            using (var sha = new SHA1CryptoServiceProvider())
                            {
                                hashPayload = sha.ComputeHash(payload);
                            }

                            return connection.SignWithAuthenticationCertificate(hashPayload, BeIDDigest.Sha1, "5945");
                        });
                }
            }


            var stsClient = (ISTSClient)_serviceProvider.GetService(typeof(ISTSClient));
            return await stsClient.RequestSAMLToken(samlEnv, new Uri(STS_URL));
            return null;
        }
        */
    }
}
