var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TranslateEnumPipe } from '@app/infrastructure/pipes/translateenum.pipe';
import { MaterialModule } from '@app/infrastructure/material.module';
import { SharedModule } from '@app/infrastructure/shared.module';
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
var PrescriptionModule = (function () {
    function PrescriptionModule() {
    }
    PrescriptionModule = __decorate([
        NgModule({
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
            providers: []
        })
    ], PrescriptionModule);
    return PrescriptionModule;
}());
export { PrescriptionModule };
//# sourceMappingURL=prescription.module.js.map