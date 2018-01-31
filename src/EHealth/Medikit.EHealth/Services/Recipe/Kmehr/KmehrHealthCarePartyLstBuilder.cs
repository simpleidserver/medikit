// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using System;
using System.Collections.Generic;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public class KmehrHealthCarePartyLstBuilder
    {
        public KmehrHealthCarePartyLstBuilder()
        {
            HcParties = new List<hcpartyType>();
        }

        internal List<hcpartyType> HcParties { get; private set; }

        public KmehrHealthCarePartyLstBuilder AddHealthCareParty(Action<KmehrHealthCarePartyBuilder> callback)
        {
            var builder = new KmehrHealthCarePartyBuilder();
            callback(builder);
            HcParties.Add(builder.Build());
            return this;
        }
    }
}
