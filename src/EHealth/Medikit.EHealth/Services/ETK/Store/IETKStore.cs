// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.ETK.Store;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Medikit.EHealth.ETK.Store
{
    public interface IETKStore
    {
        Task<ETKModel> Get(string type, string value, string applicationId);
        Task<bool> Add(string type, string value, string applicationId, X509Certificate2 certificate, string etk);
    }
}