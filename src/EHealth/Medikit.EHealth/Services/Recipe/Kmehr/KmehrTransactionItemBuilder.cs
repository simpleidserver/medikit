// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrTransactionItemBuilder
    {
        private itemType _obj;
        
        public KmehrTransactionItemBuilder(itemType obj)
        {
            _obj = obj;
        }

        public itemType Build()
        {
            return _obj;
        }
    }
}
