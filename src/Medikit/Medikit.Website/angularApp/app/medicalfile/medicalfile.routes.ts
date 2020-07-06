import { RouterModule, Routes } from '@angular/router';
import { ListMedicalfileComponent } from './list/list-medicalfile.component';

const routes: Routes = [
    { path: '', component: ListMedicalfileComponent }
];

export const MedicalfileRoutes = RouterModule.forChild(routes);