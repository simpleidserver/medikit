// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Xml.Linq;

namespace Medikit.EHealth.Services.Recipe.Request
{
    public class Page
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public long PageNumber { get; set; }
        public string Context { get; set; }

        public XElement Serialize()
        {
            var result = new XElement("page",
                new XElement("month", Month),
                new XElement("year", Year),
                new XElement("pageNumber", PageNumber));
            if (!string.IsNullOrWhiteSpace(Context))
            {
                result.Add(new XElement("context", Context));
            }

            return result;
        }
    }
}
