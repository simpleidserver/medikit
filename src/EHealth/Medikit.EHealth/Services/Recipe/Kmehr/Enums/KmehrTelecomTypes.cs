// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Medikit.EHealth.Enums;

namespace Medikit.EHealth.Services.Recipe.Kmehr.Enums
{
    public class KmehrTelecomTypes : Enumeration
    {
        public static KmehrTelecomTypes Email = new KmehrTelecomTypes(0, "email", "Electronic mail");
        public static KmehrTelecomTypes Fax = new KmehrTelecomTypes(1, "fax", "Fax");
        public static KmehrTelecomTypes Mobile = new KmehrTelecomTypes(2, "mobile", "Mobile telephone");
        public static KmehrTelecomTypes Phone = new KmehrTelecomTypes(3, "phone", "Non mobile telephone");

        public KmehrTelecomTypes(int value, string code, string description) : base(value, code, description)
        {
        }
    }
}
