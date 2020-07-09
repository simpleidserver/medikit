import { RouterModule, Routes } from '@angular/router';
import { ListPatientComponent } from './list//list-patient.component';
import { AddPatientComponent } from './add/add-patient.component';

const routes: Routes = [
    { path: '', component: ListPatientComponent },
    { path: 'add', component: AddPatientComponent }
];

export const PatientRoutes = RouterModule.forChild(routes);