var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import * as fromAppState from '@app/stores/appstate';
import * as fromPatientActions from '@app/stores/patient/patient-actions';
import * as fromPrescriptionActions from '@app/stores/pharmaprescription/prescription-actions';
import { select, Store } from '@ngrx/store';
var ListPrescriptionComponent = (function () {
    function ListPrescriptionComponent(store, medikitExtensionService) {
        this.store = store;
        this.medikitExtensionService = medikitExtensionService;
        this.filteredPatientsByNiss = [];
        this.nissFormGroup = new FormGroup({
            niss: new FormControl()
        });
        this.sessionExists = false;
        this.prescriptionIds = [];
    }
    ListPrescriptionComponent.prototype.ngOnInit = function () {
        var _this = this;
        if (this.medikitExtensionService.getEhealthSession() !== null) {
            this.sessionExists = true;
        }
        this.subscribeSessionCreated = this.medikitExtensionService.sessionCreated.subscribe(function () {
            _this.sessionExists = true;
        });
        this.subscribeSessionDropped = this.medikitExtensionService.sessionDropped.subscribe(function () {
            _this.sessionExists = false;
        });
        this.store.pipe(select(fromAppState.selectPharmaPrescriptionListResult)).subscribe(function (st) {
            if (!st) {
                return;
            }
            _this.prescriptionIds = st;
        });
        this.store.pipe(select(fromAppState.selectPatientsResult)).subscribe(function (st) {
            if (!st) {
                return;
            }
            _this.filteredPatientsByNiss = st.content;
        });
        var self = this;
        this.nissFormGroup.controls["niss"].valueChanges.subscribe(function (_) {
            self.store.dispatch(new fromPatientActions.SearchPatients(null, null, _));
        });
    };
    ListPrescriptionComponent.prototype.displayNiss = function (patient) {
        return patient.niss;
    };
    ListPrescriptionComponent.prototype.onSumitNissForm = function (form) {
        this.onSubmit(form.niss);
    };
    ListPrescriptionComponent.prototype.ngOnDestroy = function () {
        this.subscribeSessionCreated.unsubscribe();
        this.subscribeSessionDropped.unsubscribe();
    };
    ListPrescriptionComponent.prototype.onSubmit = function (niss) {
        if (!this.sessionExists) {
            return;
        }
        var session = this.medikitExtensionService.getEhealthSession();
        var loadPharmaPrescriptions = new fromPrescriptionActions.LoadPharmaPrescriptions(niss, 0, session['assertion_token']);
        this.store.dispatch(loadPharmaPrescriptions);
    };
    ListPrescriptionComponent = __decorate([
        Component({
            selector: 'list-prescription-component',
            templateUrl: './list-prescription.component.html',
            styleUrls: ['./list-prescription.component.scss']
        }),
        __metadata("design:paramtypes", [Store,
            MedikitExtensionService])
    ], ListPrescriptionComponent);
    return ListPrescriptionComponent;
}());
export { ListPrescriptionComponent };
//# sourceMappingURL=list-prescription.component.js.map