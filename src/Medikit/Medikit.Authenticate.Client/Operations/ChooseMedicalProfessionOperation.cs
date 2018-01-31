// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Authenticate.Client.Requests;
using Medikit.Authenticate.Client.Responses;
using Medikit.EHealth.Enums;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;

namespace Medikit.Authenticate.Client.Operations
{
    public class ChooseMedicalProfessionOperation : BaseOperation
    {
        public override string Code => "CHOOSE_MEDICAL_PROFESSION";
        public override string Response => "MEDICAL_PROFESSION";

        public override BrowserExtensionResponse Handle(BrowserExtensionRequest request)
        {
            var chooseMedicalProfessionRequest = request.Content.ToObject<ChooseMedicalProfessionRequest>();
            var medicalProfessions = Enumeration.GetAll<MedicalProfessions>();
            if (!medicalProfessions.Any(_ => _.Code == chooseMedicalProfessionRequest.Profession))
            {
                return BuildError(request, "medical profession doesn't exist");
            }

            UpdateAppSettings(Constants.ConfigurationNames.Profession, chooseMedicalProfessionRequest.Profession);
            return NoContent(request);
        }
    }
}
