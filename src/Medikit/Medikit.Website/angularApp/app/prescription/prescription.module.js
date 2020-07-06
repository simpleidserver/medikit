var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
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
            entryComponents: [RemovePrescriptionDialog],
            providers: [DatePipe]
        })
    ], PrescriptionModule);
    return PrescriptionModule;
}());
export { PrescriptionModule };
//# sourceMappingURL=prescription.module.js.map