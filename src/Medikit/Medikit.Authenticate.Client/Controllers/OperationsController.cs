// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Authenticate.Client.Operations;
using Medikit.Authenticate.Client.Requests;
using Medikit.Authenticate.Client.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Medikit.Authenticate.Client.Controllers
{
    [Route("operations")]
    public class OperationsController : Controller
    {
        private readonly IEnumerable<IOperation> _operations;

        public OperationsController(IEnumerable<IOperation> operations)
        {
            _operations = operations;
        }

        [HttpPost]
        public IActionResult Execute([FromBody] BrowserExtensionRequest request)
        {
            var type = request.Type;
            var operation = _operations.FirstOrDefault(_ => _.Code.Equals(type, StringComparison.InvariantCultureIgnoreCase));
            if (operation == null)
            {
                var result = new BrowserExtensionResponseGeneric<ErrorResponse>(request.Nonce, "error", new ErrorResponse { Message = "operation is unknown" });
                return new BadRequestObjectResult(result)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            {
                var result = operation.Handle(request);
                return new OkObjectResult(result);
            }            
        }
    }
}
