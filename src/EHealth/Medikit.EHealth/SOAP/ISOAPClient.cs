// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.SOAP.DTOs;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Medikit.EHealth.SOAP
{
    public interface ISOAPClient
    {
        Task<HttpResponseMessage> Send<T>(SOAPEnvelope<T> envelope, Uri uri, string action) where T : SOAPBody;
    }
}
