// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Medikit.EHealth.Enums;

namespace Medikit.EHealth.Services.Recipe.Kmehr.Enums
{
    public class KmehrAddressTypes : Enumeration
    {
        public KmehrAddressTypes CareAddress = new KmehrAddressTypes(0, "careaddress", "Care address");
        public KmehrAddressTypes Home = new KmehrAddressTypes(1, "home", "The primary home, to reach a person after business hours.");
        public KmehrAddressTypes Other = new KmehrAddressTypes(2, "other", "Other");
        public KmehrAddressTypes Vacation = new KmehrAddressTypes(3, "vacation", "A vacation home, to reach a person while on vacation.");
        public KmehrAddressTypes Work = new KmehrAddressTypes(4, "work", "A business address where the person could be contacted during working hours.");

        public KmehrAddressTypes(int value, string code, string description) : base(value, code, description)
        {
        }
    }
}
