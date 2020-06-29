import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialModule } from '@app/infrastructure/material.module';
import { SharedModule } from '@app/infrastructure/shared.module';
import { ListPatientComponent } from './list/list-patient.component';
import { PatientRoutes } from './patient.routes';
import { AvatarModule } from 'ngx-avatar';

@NgModule({
    imports: [
        CommonModule,
        PatientRoutes,
        MaterialModule,
        SharedModule,
        AvatarModule
    ],

    declarations: [
        ListPatientComponent
    ],

    exports: [
        ListPatientComponent
    ],

    providers: [ ]
})

export class PatientModule { }
