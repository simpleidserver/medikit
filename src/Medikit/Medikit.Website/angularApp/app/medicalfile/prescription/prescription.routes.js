import { RouterModule } from '@angular/router';
import { ListPrescriptionComponent } from './list/list-prescription.component';
import { AddPharmaPrescriptionComponent } from './add-pharma-prescription/add-pharma-prescription.component';
import { ViewPrescriptionComponent } from './view/view-prescription.component';
var routes = [
    { path: '', component: ListPrescriptionComponent },
    { path: 'add-pharma', component: AddPharmaPrescriptionComponent },
    { path: ':id', component: ViewPrescriptionComponent }
];
export var PrescriptionRoutes = RouterModule.forChild(routes);
//# sourceMappingURL=prescription.routes.js.map