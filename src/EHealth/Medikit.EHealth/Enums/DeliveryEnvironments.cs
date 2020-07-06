// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Medikit.EHealth.Enums
{
    public class DeliveryEnvironments : Enumeration
    {

        public static DeliveryEnvironments Ambulatory = new DeliveryEnvironments(0, "A");

        public static DeliveryEnvironments Public = new DeliveryEnvironments(1, "P");

        public static DeliveryEnvironments Hospital = new DeliveryEnvironments(2, "H");

        public static DeliveryEnvironments ResidentialCare = new DeliveryEnvironments(3, "R");


        public DeliveryEnvironments(int id, string name) : base(id, name, string.Empty) { }
    }
}
