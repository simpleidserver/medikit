import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialModule } from '@app/infrastructure/material.module';
import { SharedModule } from '@app/infrastructure/shared.module';
import { ListPatientComponent } from './list/list-patient.component';
import { PatientRoutes } from './patient.routes';
import { AvatarModule } from 'ngx-avatar';
import { AddPatientComponent } from './add/add-patient.component';
import { StoreModule } from '@ngrx/store';
import * as reducersAddPatient from './add/reducers/patient-reducer';
import { SearchAddressComponent } from '@app/infrastructure/components/searchaddress.component';
import { ViewPatientComponent } from './view/view-patient.component';

@NgModule({
    imports: [
        CommonModule,
        PatientRoutes,
        MaterialModule,
        SharedModule,
        AvatarModule,
        StoreModule.forFeature('patientForm', reducersAddPatient.PatientFormReducer)
    ],

    declarations: [
        ListPatientComponent,
        AddPatientComponent,
        SearchAddressComponent,
        ViewPatientComponent
    ],

    exports: [
        ListPatientComponent,
        AddPatientComponent
    ],

    providers: [ ]
})

export class PatientModule { }