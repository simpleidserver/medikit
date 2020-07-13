import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialModule } from '@app/infrastructure/material.module';
import { SharedModule } from '@app/infrastructure/shared.module';
import { AvatarModule } from 'ngx-avatar';
import { MessageboxViewerComponent } from './common/messageboxviewer.component';
import { InboxComponent } from './inbox/inbox.component';
import { MessageComponent } from './message.component';
import { MessageRoutes } from './message.routes';
import { SentboxComponent } from './sentbox/sentbox.component';

@NgModule({
    imports: [
        CommonModule,
        MessageRoutes,
        MaterialModule,
        SharedModule,
        AvatarModule
    ],

    declarations: [
        InboxComponent,
        MessageComponent,
        MessageboxViewerComponent,
        SentboxComponent
    ],

    entryComponents: [
    ],

    exports: [
    ],

    providers: [ ]
})

export class MessageModule { }