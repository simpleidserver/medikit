// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MediatR;
using Medikit.Api.EHealth.Application.Message.Queries.Results;
using Medikit.EHealth.Services.EHealthBox;
using System.Collections.Generic;

namespace Medikit.Api.EHealth.Application.Message.Queries
{
    public class GetMessagesQuery : IRequest<IEnumerable<MessageResult>>
    {
        public GetMessagesQuery()
        {
            StartIndex = 1;
            EndIndex = 100;
        }

        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public EHealthBoxSources Source { get; set; }
        public string AssertionToken { get; set; }
    }
}
