// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrTransactionBuilder
    {
        private transactionType _transactionType;

        public transactionType Build()
        {
            return _transactionType;
        }
    }
}
