// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.ETK.Store;
using System.Threading.Tasks;

namespace Medikit.EHealth.ETK
{
    public interface IETKService
    {
        Task<ETKModel> GetOrgETK();
        Task<ETKModel> GetRecipeETK();
        Task<ETKModel> GetKgssETK();
        Task<ETKModel> GetETK(ETKIdentifier etkIdentifier);
        Task<ETKModel> GetETK(ETKTypes type, string value, string applicationId = "");
    }
}