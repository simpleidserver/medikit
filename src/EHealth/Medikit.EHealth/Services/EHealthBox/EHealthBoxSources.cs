// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Medikit.EHealth.Services.EHealthBox
{
    public enum EHealthBoxSources
    {
        INBOX = 0, // Inbox folder.
        SENTBOX = 1, // Sent box folder.
        BININBOX = 2, // Messages moved from the inbox folder.
        BINSENTBOX = 3 // Messages moved from the sent box folder.
    }
}
