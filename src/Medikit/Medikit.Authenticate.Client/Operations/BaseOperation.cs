// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Authenticate.Client.Requests;
using Medikit.Authenticate.Client.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Medikit.Authenticate.Client.Operations
{
    public abstract class BaseOperation : IOperation
    {
        public abstract string Code { get; }
        public abstract string Response { get; }

        public abstract BrowserExtensionResponse Handle(BrowserExtensionRequest request);

        protected void UpdateAppSettings(string key, string value)
        {
            var file = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            var json = File.ReadAllText(file);
            var jObj = JsonConvert.DeserializeObject<JObject>(json);
            var token = jObj.SelectToken(key);
            if (token == null)
            {
                return;
            }

            jObj[key] = value;
            File.WriteAllText(file, jObj.ToString());
        }

        protected BrowserExtensionResponse BuildResponse<T>(BrowserExtensionRequest request, T content)
        {
            var result = new BrowserExtensionResponseGeneric<T>(request.Nonce, Response, content);
            return result;
        }

        protected BrowserExtensionResponse BuildError(BrowserExtensionRequest request, string message) 
        {
            var result = new BrowserExtensionResponseGeneric<ErrorResponse>(request.Nonce, "error", new ErrorResponse { Message = message });
            return result;
        }

        protected BrowserExtensionResponse NoContent(BrowserExtensionRequest request)
        {
            var result = new BrowserExtensionResponse(request.Nonce, Response);
            return result;
        }
    }
}
