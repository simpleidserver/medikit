// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace Medikit.Api.EHealth.Application.Message.Queries.Results
{
    public class MessageResult
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ContentType { get; set; }
        public string MimeType { get; set; }
        public bool HasAnnex { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Size { get; set; }
        public bool IsImportant { get; set; }
        public SenderResult Sender { get; set; }
        public IdentityResult Destination { get; set; }

        public class IdentityResult
        {
            public string Id { get; set; }
            public string Type { get; set; }
            public string Quality { get; set; }
        }


        public class SenderResult : IdentityResult
        {
            public string Name { get; set; }
            public string FirstName { get; set; }
        }
    }
}
