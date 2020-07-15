// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.AspNetCore.Extensions;
using Medikit.Api.EHealth.Application.Message;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Medikit.Api.AspNetCore.Controllers
{
    [Route(MedikitApiConstants.RouteNames.Messages)]
    public class MessagesController : Controller
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("inbox/search")]
        public async Task<IActionResult> SearchInboxMessages([FromBody] JObject jObj)
        {
            var query = jObj.ToGetMessagesQuery();
            var messages = await _messageService.SearchInboxMessages(query);
            return new OkObjectResult(messages.Select(_ => _.ToDto()));
        }

        [HttpPost("sentbox/search")]
        public async Task<IActionResult> SearchSentboxMessages([FromBody] JObject jObj)
        {
            var query = jObj.ToGetMessagesQuery();
            var messages = await _messageService.SearchSentboxMessages(query);
            return new OkObjectResult(messages.Select(_ => _.ToDto()));
        }

        [HttpPost("inbox/delete")]
        public async Task<IActionResult> DeleteInboxMessages([FromBody] JObject jObj)
        {
            var cmd = jObj.ToDeleteMessageCommand();
            await _messageService.DeleteInboxMessages(cmd);
            return new NoContentResult();
        }

        [HttpPost("sentbox/delete")]
        public async Task<IActionResult> DeleteSentboxMessages([FromBody] JObject jObj)
        {
            var cmd = jObj.ToDeleteMessageCommand();
            await _messageService.DeleteSentboxMessages(cmd);
            return new NoContentResult();
        }
    }
}
