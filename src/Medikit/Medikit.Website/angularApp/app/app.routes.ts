import { Routes } from '@angular/router';
import { AuthGuard } from './infrastructure/services/auth-guard.service';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', loadChildren: './home/home.module#HomeModule' },
    { path: 'prescription', loadChildren: './prescription/prescription.module#PrescriptionModule', canActivate: [AuthGuard] },
    { path: 'setting', loadChildren: './setting/setting.module#SettingModule', canActivate: [AuthGuard] },
    { path: 'status', loadChildren: './status/status.module#StatusModule' },
    { path: '**', redirectTo: '/status/404' }
];