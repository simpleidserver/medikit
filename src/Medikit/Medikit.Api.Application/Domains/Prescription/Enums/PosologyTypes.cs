// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Medikit.Api.Application.Domains
{
    public class PosologyTypes : Enumeration
    {
        public static PosologyTypes FreeText = new PosologyTypes(0, "freetext");
        public static PosologyTypes Structured = new PosologyTypes(1, "structured");

        public PosologyTypes(int id, string name) : base(id, name)
        {
        }
    }
}
