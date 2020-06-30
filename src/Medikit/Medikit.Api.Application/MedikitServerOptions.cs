// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Medikit.Api.Application
{
    public class MedikitServerOptions
    {
        public MedikitServerOptions()
        {
            SnapshotFrequency = 200;
        }

        public int SnapshotFrequency { get; set; }
        public string RootPath { get; set; }
    }
}
