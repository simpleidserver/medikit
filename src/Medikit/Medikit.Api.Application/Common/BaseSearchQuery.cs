// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Medikit.Api.Application.Common
{
    public class BaseSearchQuery
    {
        public BaseSearchQuery()
        {
            StartIndex = 0;
            Count = 100;
            Order = SearchOrders.DESC;
            OrderBy = "create_datetime";
        }

        public int StartIndex { get; set; }
        public int Count { get; set; }
        public SearchOrders Order { get; set; }
        public string OrderBy { get; set; }
    }
}
