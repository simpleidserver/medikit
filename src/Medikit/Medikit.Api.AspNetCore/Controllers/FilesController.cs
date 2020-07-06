// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.AspNetCore.Extensions;
using Medikit.Api.QRFile.Application;
using Medikit.Api.QRFile.Application.Commands;
using Medikit.Api.QRFile.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Medikit.Api.AspNetCore.Controllers
{
    [Route(MedikitApiConstants.RouteNames.Files)]
    public class FilesController : Controller
    {
        private readonly IQRFileService _fileService;

        public FilesController(IQRFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] JObject jObj)
        {
            // TODO : Add security : User
            var command = BuildTransferFileCommand(jObj);
            var id = await _fileService.Transfer(command, CancellationToken.None);
            var location = Request.GetAbsoluteUriWithVirtualPath();
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { id = id , location = $"{location}/files/{id}"}),
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            // TODO : Add security : User
            try
            {
                var result = await _fileService.Get(id, CancellationToken.None);
                return new OkObjectResult(new {  file = result });
            }
            catch(UnknownQRFileException)
            {
                return NotFound();
            }
        }

        private static TransferQRFileCommand BuildTransferFileCommand(JObject jObj)
        {
            var result = new TransferQRFileCommand();
            var values = jObj.ToObject<Dictionary<string, object>>();
            string file;
            if (values.TryGet("file", out file))
            {
                result.File = file;
            }

            return result;
        }
    }
}
