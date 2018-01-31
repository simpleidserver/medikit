// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SOAP.DTOs;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Medikit.EHealth.SOAP
{
    public class SOAPClient : ISOAPClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly EHealthOptions _options;

        public SOAPClient(IHttpClientFactory httpClientFactory, IOptions<EHealthOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
        }

        public Task<HttpResponseMessage> Send<T>(SOAPEnvelope<T> envelope, Uri uri, string action) where T : SOAPBody
        {
            var serializedEnvelope = envelope.Serialize(true);
            var content = new StringContent(serializedEnvelope, Encoding.UTF8, "text/xml");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = uri,
                Content = content
            };
            if (!string.IsNullOrWhiteSpace(action))
            {
                request.Headers.Add("SOAPAction", $"\"{action}\"");
            }

            request.Headers.Add("User-Agent", $"{_options.ProductName} ({_options.Version})");
            var httpClient = _httpClientFactory.CreateClient("soapClient");
            httpClient.Timeout = _options.Timeout;
            return httpClient.SendAsync(request);
        }
    }
}
