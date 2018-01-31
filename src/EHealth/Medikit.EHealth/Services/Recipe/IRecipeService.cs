// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Medikit.EHealth.SAML.DTOs;
using Medikit.EHealth.Services.Recipe.Request;
using Medikit.EHealth.Services.Recipe.Response;
using System.Threading.Tasks;

namespace Medikit.EHealth.Services.Recipe
{
    public interface IRecipeService
    {
        Task<GetPrescriptionResult> GetPrescription(string rid);
        Task<GetPrescriptionResult> GetPrescription(string rid, SAMLAssertion assertion);
        Task<ListOpenRidsResult> GetOpenedPrescriptions(string patientId, Page page);
        Task<ListOpenRidsResult> GetOpenedPrescriptions(string patientId, Page page, SAMLAssertion assertion);
    }
}
