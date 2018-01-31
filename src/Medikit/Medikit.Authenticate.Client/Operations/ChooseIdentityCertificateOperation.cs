// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Authenticate.Client.Requests;
using Medikit.Authenticate.Client.Responses;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Medikit.Authenticate.Client.Operations
{
    public class ChooseIdentityCertificateOperation : BaseOperation
    {
        private IConfiguration _configuration;

        public ChooseIdentityCertificateOperation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override string Code => "CHOOSE_IDENTITY_CERTIFICATE";
        public override string Response => "IDENTITY_CERTIFICATE";

        public override BrowserExtensionResponse Handle(BrowserExtensionRequest request)
        {
            var chooseCertificate = request.Content.ToObject<ChooseIdentityCertificateRequest>();
            var certificateStorePath = _configuration[Constants.ConfigurationNames.CertificateStorePath];
            var path = Path.Combine(certificateStorePath, chooseCertificate.Certificate);
            if (!File.Exists(path))
            {
                return BuildError(request, "identity certificate doesn't exist");
            }

            try
            {
                var col = new X509Certificate2Collection();
                col.Import(path, chooseCertificate.Password, X509KeyStorageFlags.Exportable);
            }
            catch
            {
                return BuildError(request, "password is invalid");
            }

            UpdateAppSettings(Constants.ConfigurationNames.IdentityCertificateStore, chooseCertificate.Certificate);
            UpdateAppSettings(Constants.ConfigurationNames.IdentityCertificateStorePassword, chooseCertificate.Password);
            return NoContent(request);
        }
    }
}
