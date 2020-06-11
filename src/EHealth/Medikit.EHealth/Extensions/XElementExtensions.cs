// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Xml.Linq;

namespace Medikit.EHealth.Extensions
{
    public static class XElementExtensions
    {
        public static string SerializeToString(this XElement elt)
        {
            var r = elt.CreateReader();
            r.MoveToContent();
            return r.ReadOuterXml();
        }
    }
}
