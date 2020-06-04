import { RouterModule } from '@angular/router';
import { ListSettingComponent } from './list/list-setting.component';
import { ExtensionComponent } from './extension/extension.component';
var routes = [
    { path: '', component: ListSettingComponent },
    { path: 'extension', component: ExtensionComponent }
];
export var SettingRoutes = RouterModule.forChild(routes);
//# sourceMappingURL=setting.routes.js.map