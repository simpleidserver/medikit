// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Authenticate.Client.Requests;
using Medikit.Authenticate.Client.Responses;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Medikit.Authenticate.Client.Operations
{
    public class GetIdentityCertificateOperation : BaseOperation
    {
        private IConfiguration _configuration;

        public GetIdentityCertificateOperation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override string Code => "GET_IDENTITY_CERTIFICATE";

        public override string Response => "IDENTITY_CERTIFICATE";

        public override BrowserExtensionResponse Handle(BrowserExtensionRequest request)
        {
            var getCertificate = request.Content.ToObject<GetIdentityCertificateRequest>();
            var certificateStorePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Certificates");
            var path = Path.Combine(certificateStorePath, getCertificate.Certificate);
            if (!File.Exists(path))
            {
                return BuildError(request, "identity certificate doesn't exist");
            }

            try
            {
                var col = new X509Certificate2Collection();
                col.Import(path, getCertificate.Password, X509KeyStorageFlags.Exportable);
            }
            catch
            {
                return BuildError(request, "password is invalid");
            }

            string location;
            var apiUrl = _configuration["ApiUrl"];
            using (var httpClient = new HttpClient())
            {
                var httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{apiUrl}/files/transfer"),
                    Content =  new StringContent(JsonConvert.SerializeObject(new { file = Convert.ToBase64String(File.ReadAllBytes(path)) }), Encoding.UTF8, "application/json")
                };
                var httpResponse = httpClient.SendAsync(httpRequest).Result;
                var json = httpResponse.Content.ReadAsStringAsync().Result.ToString();
                location = JsonConvert.DeserializeObject<JObject>(json)["location"].ToString();
            }
            
            return BuildResponse(request, new GetIdentityCertificateResponse(location, getCertificate.Password, getCertificate.Certificate));
        }
    }
}
