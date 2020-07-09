import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialModule } from '@app/infrastructure/material.module';
import { SharedModule } from '@app/infrastructure/shared.module';
import { AvatarModule } from 'ngx-avatar';
import { AddMedicalfileDialog, ListMedicalfileComponent } from './list/list-medicalfile.component';
import { MedicalfileRoutes } from './medicalfile.routes';
import { RemovePrescriptionDialog, ViewMedicalfileComponent } from './view/view-medicalfile.component';

@NgModule({
    imports: [
        CommonModule,
        MedicalfileRoutes,
        MaterialModule,
        SharedModule,
        AvatarModule
    ],

    declarations: [
        ListMedicalfileComponent,
        ViewMedicalfileComponent,
        AddMedicalfileDialog,
        RemovePrescriptionDialog
    ],

    entryComponents: [
        AddMedicalfileDialog,
        RemovePrescriptionDialog
    ],

    exports: [
    ],

    providers: [ ]
})

export class MedicalfileModule { }