import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TranslateEnumPipe } from '@app/pipes/translateenum.pipe';
import { MaterialModule } from '@app/shared/material.module';
import { SharedModule } from '@app/shared/shared.module';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { AddPharmaPrescriptionComponent } from './add-pharma-prescription/add-pharma-prescription.component';
import { PharmaPrescriptionFormEffects } from './add-pharma-prescription/effects/pharma-prescription';
import * as reducersAddPharmaPrescription from './add-pharma-prescription/reducers/pharma-prescription-reducer';
import { ListPharmaPrescriptionEffects } from './list/effects/pharma-prescription';
import { ListPrescriptionComponent } from './list/list-prescription.component';
import * as reducersListPharmaPrescription from './list/reducers/pharma-prescription-reducer';
import { PrescriptionRoutes } from './prescription.routes';
import { ViewPharmaPrescriptionEffects } from './view/effects/pharma-prescription';
import * as reducersViewPharmaPrescription from './view/reducers/pharma-prescription-reducer';
import { ViewPrescriptionComponent } from './view/view-prescription.component';
import { PrescriptionViewerComponent } from './viewer/prescription-viewer.component';

@NgModule({
    imports: [
        CommonModule,
        PrescriptionRoutes,
        MaterialModule,
        SharedModule,
        EffectsModule.forRoot([ListPharmaPrescriptionEffects, PharmaPrescriptionFormEffects, ViewPharmaPrescriptionEffects]),
        StoreModule.forRoot({
            pharmaPrescriptionForm: reducersAddPharmaPrescription.PharmaPrescriptionFormReducer,
            pharmaPrescriptionLst: reducersListPharmaPrescription.ListPharmaPrescriptionReducer,
            pharmaPrescriptionView: reducersViewPharmaPrescription.ViewPharmaPrescriptionReducer
        }),
        StoreDevtoolsModule.instrument({
            maxAge: 10
        })
    ],

    declarations: [
        ListPrescriptionComponent,
        AddPharmaPrescriptionComponent,
        ViewPrescriptionComponent,
        PrescriptionViewerComponent,
        TranslateEnumPipe
    ],

    providers: [ ]
})

export class PrescriptionModule { }