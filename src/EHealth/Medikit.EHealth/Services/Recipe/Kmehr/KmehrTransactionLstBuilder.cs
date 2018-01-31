// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using System;
using System.Collections.Generic;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrTransactionLstBuilder
    {
        internal KmehrTransactionLstBuilder()
        {
            TransactionLst = new List<transactionType>();
        }
        
        internal List<transactionType> TransactionLst { get; private set; }

        public KmehrTransactionLstBuilder AddTransaction(Action<KmehrTransactionBuilder> callback)
        {
            var builder = new KmehrTransactionBuilder();
            callback(builder);
            TransactionLst.Add(builder.Build());
            return this;
        }
    }
}
