var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
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
var PatientModule = (function () {
    function PatientModule() {
    }
    PatientModule = __decorate([
        NgModule({
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
            providers: []
        })
    ], PatientModule);
    return PatientModule;
}());
export { PatientModule };
//# sourceMappingURL=patient.module.js.map