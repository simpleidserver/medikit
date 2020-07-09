import { RouterModule, Routes } from '@angular/router';
import { AddPharmaPrescriptionComponent } from './add/add-pharma-prescription.component';
import { ViewPrescriptionComponent } from './view/view-prescription.component';

const routes: Routes = [
    { path: 'add', component: AddPharmaPrescriptionComponent },
    { path: ':rid/view', component: ViewPrescriptionComponent }
];

export const PrescriptionRoutes = RouterModule.forChild(routes);