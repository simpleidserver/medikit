// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Common.Application.Queries;
using System.Collections.Generic;

namespace Medikit.Api.AspNetCore.Extensions
{
    public static class BaseSearchQueryExtensions
    {
        public static void ExtractSearchParameters(this BaseSearchQuery query, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            int startIndex, count;
            string orderBy;
            SearchOrders order;
            if (parameters.TryGet("start_index", out startIndex))
            {
                query.StartIndex = startIndex;
            }

            if (parameters.TryGet("count", out count))
            {
                query.Count = count;
            }

            if (parameters.TryGet("order_by", out orderBy))
            {
                query.OrderBy = orderBy;
            }

            if (parameters.TryGet("order", out order))
            {
                query.Order = order;
            }
        }
    }
}
