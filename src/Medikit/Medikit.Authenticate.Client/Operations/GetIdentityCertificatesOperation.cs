// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Authenticate.Client.Requests;
using Medikit.Authenticate.Client.Responses;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace Medikit.Authenticate.Client.Operations
{
    public class GetIdentityCertificatesOperation : BaseOperation
    {
        private readonly IConfiguration _configuration;

        public GetIdentityCertificatesOperation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override string Code => "GET_IDENTITIY_CERTIFICATES";
        public override string Response => "IDENTITY_CERTIFICATES";

        public override BrowserExtensionResponse Handle(BrowserExtensionRequest request)
        {
            var certificateStorePath = _configuration[Constants.ConfigurationNames.CertificateStorePath];
            var regexTest = new Func<string, bool>(i => i.StartsWith("SSIN=", StringComparison.InvariantCultureIgnoreCase));
            var files = Directory.GetFiles(certificateStorePath).Select(_ => Path.GetFileName(_)).Where(regexTest).ToList();
            return BuildResponse(request, new IdentityCertificatesResponse(_configuration[Constants.ConfigurationNames.IdentityCertificateStore], files));
        }
    }
}
