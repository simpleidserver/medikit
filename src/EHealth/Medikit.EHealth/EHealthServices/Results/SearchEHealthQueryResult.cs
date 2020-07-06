// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace Medikit.EHealth.EHealthServices.Results
{
    public class SearchEHealthQueryResult<T> where T : class
    {
        public int StartIndex { get; set; }
        public int Count { get; set; }
        public ICollection<T> Content { get; set; }
    }
}
