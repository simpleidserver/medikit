// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;


namespace Medikit.Api.Common.Application.Persistence
{
    public class PagedResult<T>
    {
        public int StartIndex { get; set; }
        public int Count { get; set; }
        public int TotalLength { get; set; }
        public ICollection<T> Content { get; set; }
    }
}
