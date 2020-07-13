// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.EHealth.Application.Message.Queries.Results;
using Medikit.EHealth.Services.EHealthBox.Response;

namespace Medikit.Api.EHealth.Application.Extensions
{
    public static class MappingExtensions
    {
        public static MessageResult ToResult(this EHealthBoxMessageResponse message)
        {
            return new MessageResult
            {
                Id = message.MessageId,
                ContentType = message.ContentSpecification.ContentType,
                IsImportant = message.ContentSpecification.IsImportant,
                MimeType = message.ContentInfo.MimeType,
                Title = message.ContentInfo.Title,
                HasAnnex = message.ContentInfo.HasAnnex,
                PublicationDate = message.MessageInfo.PublicationDate,
                ExpirationDate = message.MessageInfo.ExpirationDate,
                Size = message.MessageInfo.Size,
                Destination = new MessageResult.IdentityResult
                {
                    Id = message.Destination.Id,
                    Quality = message.Destination.Quality,
                    Type = message.Destination.Type
                },
                Sender = new MessageResult.SenderResult
                {
                    FirstName = message.Sender.FirstName,
                    Id = message.Sender.Id,
                    Name = message.Sender.Name,
                    Quality = message.Sender.Quality,
                    Type = message.Sender.Type
                }
            };
        }
    }
}
