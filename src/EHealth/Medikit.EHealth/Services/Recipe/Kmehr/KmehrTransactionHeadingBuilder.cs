// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Services.Recipe.Kmehr.Xsd;
using System;
using System.Collections.Generic;

namespace Medikit.EHealth.Services.Recipe.Kmehr
{
    public partial class KmehrTransactionHeadingBuilder
    {
        private headingType _obj;

        public headingType Build()
        {
            return _obj;
        }

        public KmehrTransactionHeadingBuilder AddTransactionItem(Action<KmehrTransactionItemBuilder> callback)
        {
            var itemType = new itemType();
            var builder = new KmehrTransactionItemBuilder(itemType);
            callback(builder);
            var objs = new List<object>();
            if (_obj.Items != null)
            {
                objs.AddRange(_obj.Items);
            }

            objs.Add(builder.Build());
            _obj.Items = objs.ToArray();
            return this;
        }
    }
}
