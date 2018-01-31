import { RouterModule, Routes } from '@angular/router';
import { ListSettingComponent } from './list/list-setting.component';
import { ExtensionComponent } from './extension/extension.component';

const routes: Routes = [
    { path: '', component: ListSettingComponent },
    { path: 'extension', component: ExtensionComponent }
];

export const SettingRoutes = RouterModule.forChild(routes);