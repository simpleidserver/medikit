// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Authenticate.Client.Operations;
using Medikit.Authenticate.Client.Requests;
using Medikit.Authenticate.Client.Responses;
using Medikit.EHealth.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Medikit.Authenticate.Client
{
    public class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;

        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
            var request = Read();
            var type = request.Type.ToUpperInvariant();
            var lst = new List<IOperation>
                {
                    new PingOperation(),
                    new ChooseIdentityCertificateOperation(_configuration),
                    new ChooseMedicalProfessionOperation(),
                    new EhealthCertificateAuthenticateOperation(_serviceProvider),
                    new EidAuthenticateOperation(_serviceProvider),
                    new GetIdentityCertificatesOperation(_configuration),
                    new GetMedicalProfessionsOperation(_configuration)
                };
            var operation = lst.FirstOrDefault(_ => _.Code.Equals(type, StringComparison.InvariantCultureIgnoreCase));
            if (operation == null)
            {
                var result = new BrowserExtensionResponseGeneric<ErrorResponse>(request.Nonce, "error", new ErrorResponse { Message = "operation is unknown" });
                SendResponse(result);
                return;
            }
            {
                var result = operation.Handle(request);
                SendResponse(result);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var certificatesPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Certificates");
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .Build();
            services.AddEHealth(_ =>
            {
                _.IdentityCertificateStore = Path.Combine(certificatesPath, _configuration[Constants.ConfigurationNames.IdentityCertificateStore]);
                _.IdentityCertificateStorePassword = _configuration[Constants.ConfigurationNames.IdentityCertificateStorePassword];
                _.OrgCertificateStore = Path.Combine(certificatesPath, _configuration[Constants.ConfigurationNames.OrgCertificateStoreFile]);
                _.OrgCertificateStorePassword = _configuration[Constants.ConfigurationNames.OrgCertificateStorePassword];
                _.IdentityProfession = Enumeration.Get<MedicalProfessions>(_configuration[Constants.ConfigurationNames.Profession]);
            });
        }

        private static void SendResponse(BrowserExtensionResponse response)
        {
            var json = JsonConvert.SerializeObject(response);
            var bytes = Encoding.UTF8.GetBytes(json);
            var stdout = Console.OpenStandardOutput();
            stdout.WriteByte((byte)((bytes.Length >> 0) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 8) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 16) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 24) & 0xFF));
            stdout.Write(bytes, 0, bytes.Length);
            stdout.Flush();
        }

        private static BrowserExtensionRequest Read()
        {
            var stdin = Console.OpenStandardInput();
            var lengthBytes = new byte[4];
            stdin.Read(lengthBytes, 0, 4);
            var length = BitConverter.ToInt32(lengthBytes, 0);
            var buffer = new char[length];
            using (var reader = new StreamReader(stdin))
            {
                while (reader.Peek() >= 0)
                {
                    reader.Read(buffer, 0, buffer.Length);
                }
            }

            return JsonConvert.DeserializeObject<BrowserExtensionRequest>(new string(buffer));
        }
    }
}
