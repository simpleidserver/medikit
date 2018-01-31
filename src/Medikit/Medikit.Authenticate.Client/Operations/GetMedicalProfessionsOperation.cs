// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Authenticate.Client.Requests;
using Medikit.Authenticate.Client.Responses;
using Medikit.EHealth.Enums;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Medikit.Authenticate.Client.Operations
{
    public class GetMedicalProfessionsOperation : BaseOperation
    {
        private readonly IConfiguration _configuration;

        public GetMedicalProfessionsOperation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override string Code => "GET_MEDICAL_PROFESSIONS";
        public override string Response => "MEDICAL_PROFESSIONS";

        public override BrowserExtensionResponse Handle(BrowserExtensionRequest request)
        {
            var professions = Enumeration.GetAll<MedicalProfessions>();
            return BuildResponse(request, new GetMedicalProfessionsResponse
            {
                CurrentProfession = _configuration[Constants.ConfigurationNames.Profession],
                Professions = professions.Select(_ => new MedicalProfessionResponse
                {
                    Code = _.Value,
                    DisplayName = _.Description,
                    Namespace = _.Code
                }).ToList()
            });
        }
    }
}
