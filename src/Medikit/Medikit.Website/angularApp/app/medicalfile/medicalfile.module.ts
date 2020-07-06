import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialModule } from '@app/infrastructure/material.module';
import { SharedModule } from '@app/infrastructure/shared.module';
import { MedicalfileRoutes } from './medicalfile.routes';

@NgModule({
    imports: [
        CommonModule,
        MedicalfileRoutes,
        MaterialModule,
        SharedModule
    ],

    declarations: [
    ],

    exports: [
    ],

    providers: [ ]
})

export class MedicalfileModule { }