// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Exceptions;
using Medikit.Api.Application.File;
using Medikit.Api.Application.File.Commands;
using Medikit.Api.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Medikit.Api.AspNetCore.Controllers
{
    [Route("files")]
    public class FilesController : Controller
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] JObject jObj)
        {
            // TODO : Add security : User
            var command = BuildTransferFileCommand(jObj);
            var id = await _fileService.Transfer(command);
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
                var result = await _fileService.Get(id);
                return new OkObjectResult(new {  file = result });
            }
            catch(UnknownFileException)
            {
                return NotFound();
            }
        }

        private static TransferFileCommand BuildTransferFileCommand(JObject jObj)
        {
            var result = new TransferFileCommand();
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
