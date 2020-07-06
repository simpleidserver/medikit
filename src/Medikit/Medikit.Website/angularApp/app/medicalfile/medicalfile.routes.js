import { RouterModule } from '@angular/router';
import { ListPatientComponent } from './list//list-patient.component';
import { AddPatientComponent } from './add/add-patient.component';
import { ViewPatientComponent } from './view/view-patient.component';
var routes = [
    { path: '', component: ListPatientComponent },
    { path: 'add', component: AddPatientComponent },
    { path: ':id', component: ViewPatientComponent }
];
export var PatientRoutes = RouterModule.forChild(routes);
//# sourceMappingURL=patient.routes.js.map