// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Medikit.Api.Application.Services.Parameters
{
    public class SearchAmpRequest
    {
        public string ProductName { get; set; }
        public string DeliveryEnvironment { get; set; }
        public bool? IsCommercialised { get; set; }
        public int StartIndex { get; set; }
        public int Count { get; set; }
    }
}
