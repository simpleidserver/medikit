import { RouterModule, Routes } from '@angular/router';
import { ListMedicalfileComponent } from './list/list-medicalfile.component';
import { ViewMedicalfileComponent } from './view/view-medicalfile.component';

const routes: Routes = [
    { path: '', component: ListMedicalfileComponent },
    { path: ':id', component: ViewMedicalfileComponent },
    { path: ':id/prescription', loadChildren: './prescription/prescription.module#PrescriptionModule' }
];

export const MedicalfileRoutes = RouterModule.forChild(routes);