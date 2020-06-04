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
import { MatSnackBar } from '@angular/material';
import { select, Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import { LoadPharmaPrescriptions } from './actions/pharma-prescription';
var ListPrescriptionComponent = (function () {
    function ListPrescriptionComponent(store, translateService, medikitExtensionService, snackBar) {
        this.store = store;
        this.translateService = translateService;
        this.medikitExtensionService = medikitExtensionService;
        this.snackBar = snackBar;
        this.prescriptionIds = [];
        this.searchForm = new FormGroup({
            niss: new FormControl()
        });
    }
    ListPrescriptionComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.isExtensionInstalled = this.medikitExtensionService.isExtensionInstalled();
        var session = this.medikitExtensionService.getEhealthSession();
        if (session) {
            this.isEhealthSessionActive = true;
        }
        else {
            this.isEhealthSessionActive = false;
        }
        this.store.pipe(select('pharmaPrescriptionLst')).subscribe(function (st) {
            _this.prescriptionIds = st.prescriptionIds;
        });
    };
    ListPrescriptionComponent.prototype.authenticateEHEALTH = function () {
        if (!this.medikitExtensionService.isExtensionInstalled()) {
            return;
        }
        var self = this;
        this.medikitExtensionService.getEhealthCertificateAuth().subscribe(function () {
            self.isEhealthSessionActive = true;
        });
    };
    ListPrescriptionComponent.prototype.disconnectEHEALTH = function () {
        this.medikitExtensionService.disconnect();
        this.isEhealthSessionActive = false;
    };
    ListPrescriptionComponent.prototype.onSubmit = function (form) {
        var undo = this.translateService.instant('undo');
        if (!this.medikitExtensionService.isExtensionInstalled()) {
            this.snackBar.open(this.translateService.instant('extension-not-installed'), undo, {
                duration: 2000,
            });
            return;
        }
        var session = this.medikitExtensionService.getEhealthSession();
        if (session == null) {
            this.snackBar.open(this.translateService.instant('no-active-session'), undo, {
                duration: 2000,
            });
            return;
        }
        var loadPharmaPrescriptions = new LoadPharmaPrescriptions(form.niss, 0, session['assertion_token']);
        this.store.dispatch(loadPharmaPrescriptions);
    };
    ListPrescriptionComponent = __decorate([
        Component({
            selector: 'list-prescription-component',
            templateUrl: './list-prescription.component.html',
            styleUrls: ['./list-prescription.component.scss']
        }),
        __metadata("design:paramtypes", [Store, TranslateService, MedikitExtensionService, MatSnackBar])
    ], ListPrescriptionComponent);
    return ListPrescriptionComponent;
}());
export { ListPrescriptionComponent };
//# sourceMappingURL=list-prescription.component.js.map