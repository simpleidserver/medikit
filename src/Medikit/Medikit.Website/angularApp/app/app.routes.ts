import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', loadChildren: './home/home.module#HomeModule' },
    { path: 'prescription', loadChildren: './prescription/prescription.module#PrescriptionModule' },
    { path: 'setting', loadChildren: './setting/setting.module#SettingModule' },
    { path: '**', redirectTo: '/status/404' }
];