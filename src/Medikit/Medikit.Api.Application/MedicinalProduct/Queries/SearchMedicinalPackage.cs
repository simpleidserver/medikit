// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.Api.Application.Domains;

namespace Medikit.Api.Application.MedicinalProduct.Queries
{
    public class SearchMedicinalPackage
    {
        public SearchMedicinalPackage()
        {
            DeliveryEnvironment = DeliveryEnvironments.Public;
            IsCommercialised = true;
            StartIndex = 0;
            Count = 10;
        }

        public string SearchText { get; set; }
        public DeliveryEnvironments DeliveryEnvironment { get; set; }
        public bool? IsCommercialised { get; set; }
        public int StartIndex { get; set; }
        public int Count { get; set; }
        public string Language { get; set; }
    }
}
