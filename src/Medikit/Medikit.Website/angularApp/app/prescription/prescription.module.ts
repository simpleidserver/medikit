import { CommonModule, DatePipe } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialModule } from '@app/infrastructure/material.module';
import { TranslateEnumPipe } from '@app/infrastructure/pipes/translateenum.pipe';
import { SharedModule } from '@app/infrastructure/shared.module';
import { StoreModule } from '@ngrx/store';
import { AddPharmaPrescriptionComponent } from './add-pharma-prescription/add-pharma-prescription.component';
import * as reducersAddPharmaPrescription from './add-pharma-prescription/reducers/pharma-prescription-reducer';
import { ListPrescriptionComponent, RemovePrescriptionDialog } from './list/list-prescription.component';
import { PrescriptionRoutes } from './prescription.routes';
import { ViewPrescriptionComponent } from './view/view-prescription.component';
import { PrescriptionViewerComponent } from './viewer/prescription-viewer.component';

@NgModule({
    imports: [
        CommonModule,
        PrescriptionRoutes,
        MaterialModule,
        SharedModule,
        StoreModule.forFeature('pharmaPrescriptionForm', reducersAddPharmaPrescription.PharmaPrescriptionFormReducer)
    ],
    declarations: [
        ListPrescriptionComponent,
        AddPharmaPrescriptionComponent,
        ViewPrescriptionComponent,
        PrescriptionViewerComponent,
        RemovePrescriptionDialog,
        TranslateEnumPipe
    ],
    entryComponents: [ RemovePrescriptionDialog ],
    providers: [ DatePipe ]
})

export class PrescriptionModule { }