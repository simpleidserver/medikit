import { RouterModule, Routes } from '@angular/router';
import { ListPatientComponent } from './list//list-patient.component';

const routes: Routes = [
    { path: '', component: ListPatientComponent }
];

export const PatientRoutes = RouterModule.forChild(routes);