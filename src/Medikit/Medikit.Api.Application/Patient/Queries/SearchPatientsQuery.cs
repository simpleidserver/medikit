// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Common;

namespace Medikit.Api.Application.Patient.Queries
{
    public class SearchPatientsQuery : BaseSearchQuery
    {
        public SearchPatientsQuery()
        {
            StartIndex = 0;
            Count = 100;
        }

        public string Niss { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
