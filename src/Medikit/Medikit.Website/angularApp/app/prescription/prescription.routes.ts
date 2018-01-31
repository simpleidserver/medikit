import { RouterModule, Routes } from '@angular/router';
import { ListPrescriptionComponent } from './list/list-prescription.component';
import { AddPharmaPrescriptionComponent } from './add-pharma-prescription/add-pharma-prescription.component';
import { ViewPrescriptionComponent } from './view/view-prescription.component';

const routes: Routes = [
    { path: '', component: ListPrescriptionComponent },
    { path: 'add-pharma', component: AddPharmaPrescriptionComponent },
    { path: ':id', component: ViewPrescriptionComponent }
];

export const PrescriptionRoutes = RouterModule.forChild(routes);