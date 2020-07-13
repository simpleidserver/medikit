import { RouterModule, Routes } from '@angular/router';
import { InboxComponent } from './inbox/inbox.component';
import { MessageComponent } from './message.component';
import { SentboxComponent } from './sentbox/sentbox.component';

const routes: Routes = [
    { path: '', redirectTo: 'inbox', pathMatch: 'full' },
    {
        path: '', component: MessageComponent, children: [
            { path: 'inbox', component: InboxComponent }, 
            { path: 'sentbox', component: SentboxComponent }
        ]
    }
];

export const MessageRoutes = RouterModule.forChild(routes);