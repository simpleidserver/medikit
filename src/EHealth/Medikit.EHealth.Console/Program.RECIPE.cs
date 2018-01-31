// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe;
using Medikit.EHealth.Services.Recipe.Request;
using System.Threading.Tasks;

namespace Medikit.EHealth.Console
{
    public partial class Program
    {
        public static async Task GetPrescription()
        {
            var recipeService = (IRecipeService)_serviceProvider.GetService(typeof(IRecipeService));
            await recipeService.GetPrescription("BEP0SGEZ8L77");
        }

        public static async Task GetOpenedPrescriptions()
        {
            var recipeService = (IRecipeService)_serviceProvider.GetService(typeof(IRecipeService));
            await recipeService.GetOpenedPrescriptions("76020727360", new Page());
        }

        /*
        public static async Task CreatePrescription()
        {
            var issueInstant = DateTime.UtcNow;
            var identityCertificate = GetIdentityCertificate();
            var orgCertificate = GetOrgCertificate();
            var kgssETK = await GetETKKGSSRequest();
            var recipeETK = await GetETKRECIPERequest();
            var medikitETK = await GetETKMEDIKITRequest();
            var kgssKeyResponse = await GetKGSSMedikitRequest(kgssETK, medikitETK);
            var payload = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "sample-prescription.xml"));
            var compressedPayload = Compress(payload);
            byte[] encryptedPrescription = new byte[0];
            using (var aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(kgssKeyResponse.NewKey);
                encryptedPrescription = Seal(compressedPayload, aes, kgssKeyResponse.NewKeyIdentifier);
            }

            var symKey = DES.Create();
            symKey.Padding = PaddingMode.PKCS7;
            symKey.Mode = CipherMode.CBC;
            var prescriptionParameter = new CreatePrescriptionParameter
            {
                Prescription = Convert.ToBase64String(encryptedPrescription),
                PrescriptionType = "P0",
                FeedbackRequested = false,
                KeyId = kgssKeyResponse.NewKeyIdentifier,
                SymmKey = Convert.ToBase64String(symKey.Key),
                PatientId = "76020727360",
                ExpirationDate = "2020-05-25",
                Vision = ""
            };
            var serializedPrescriptionParameter = prescriptionParameter.Serialize();
            byte[] encryptedPrescriptionParameter = Seal(serializedPrescriptionParameter, recipeETK);
            var createPrescriptionRequest = new CreatePrescriptionRequest
            {
                IssueInstant = issueInstant,
                Id = $"id{Guid.NewGuid().ToString()}",
                ProgramId = "Medikit",
                AdministrativeInformation = new CreatePrescriptionAdministrativeInformationType
                {
                    KeyIdentifier = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(kgssKeyResponse.NewKeyIdentifier)),
                    PrescriptionVersion = "kmehr_1.29",
                    ReferenceSourceVersion = "samv2:ABCDE999999999999",
                    PrescriptionType = "P0"
                },
                SecuredCreatePrescriptionRequest = new SecuredContentType
                {
                    SecuredContent = Convert.ToBase64String(encryptedPrescriptionParameter)
                }
            };
            var client = (IRecipePrescriberClient)_serviceProvider.GetService(typeof(IRecipePrescriberClient));
            var identityResponse = await BuildSTSIdentityRequest();
            var assertion = identityResponse.Body.Response.Assertion;
            await client.CreatePrescription(assertion, (payload) =>
            {
                var privateKey = orgCertificate.GetRSAPrivateKey();
                var result = privateKey.SignData(payload, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
                return result;
            }, issueInstant, createPrescriptionRequest, new Uri("https://services-acpt.ehealth.fgov.be/Recip-e/v4/Prescriber"));
        }

        private static byte[] Compress(byte[] input)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var stream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                {
                    stream.Write(input);
                    return memoryStream.ToArray();
                }
            }
        }
        */
    }
}
